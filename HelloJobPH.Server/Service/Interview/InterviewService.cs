using HelloJobPH.Server.Data;
using HelloJobPH.Server.Service.Email;
using HelloJobPH.Server.Utility;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
using HelloJobPH.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HelloJobPH.Server.Service.Interview
{
    public class InterviewService : IInterviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        public InterviewService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<List<InterviewListDtos>> FinalList()
        {
            var userId = Utilities.GetUserId();
            if (userId is null)
                throw new Exception("User not found!!");
                
            try
            {
                var result = await _context.Application
                    .Where(a => a.ApplicationStatus == ApplicationStatus.Final && a.IsDeleted ==0&&a.HumanResourceId == userId)
                    .Select(a => new InterviewListDtos
                    {
                        ApplicationId = a.ApplicationId,
                        //ResumeUrl = a.ResumeUrl,
                        Firstname = a.Applicant.Firstname,
                        Lastname = a.Applicant.Surname,
                        Email = a.Applicant.UserAccount.Email,
                        JobTitle = a.JobPosting.Title,
                        Type = a.JobPosting.EmploymentType,
                        DateApplied = a.DateApply,
                        TimeInterview = a.Interview.ScheduledTime,
                        DateInterview = a.Interview.ScheduledDate,
                        Status = a.ApplicationStatus
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<InterviewListDtos>> InitialList()
        {
            var userId = Utilities.GetUserId();
            if (userId is null)
                throw new Exception("User not found!!");
            try
            {
                var result = await _context.Application
                    .Where(a => a.ApplicationStatus == ApplicationStatus.Initial && a.IsDeleted == 0 && a.HumanResourceId == userId)
                    .Select(a => new InterviewListDtos
                    {
                        ApplicationId = a.ApplicationId,
                        //ResumeUrl = a.ResumeUrl,
                        Firstname = a.Applicant.Firstname,
                        Lastname = a.Applicant.Surname,
                        Email = a.Applicant.UserAccount.Email,
                        JobTitle = a.JobPosting.Title,
                        Type = a.JobPosting.EmploymentType,
                        DateApplied = a.DateApply,
                        TimeInterview = a.Interview != null ? a.Interview.ScheduledTime : null,
                        DateInterview = a.Interview != null ? a.Interview.ScheduledDate : null,
                        Status = a.ApplicationStatus
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<InterviewListDtos>> TechnicalList()
        {
            var userId = Utilities.GetUserId();
            if (userId is null)
                throw new Exception("User not found!!");
            try
            {
                var result = await _context.Application
                    .Where(a => a.ApplicationStatus == ApplicationStatus.Technical && a.IsDeleted == 0 && a.HumanResourceId == userId)
                    .Select(a => new InterviewListDtos
                    {
                        ApplicationId = a.ApplicationId,
                        //ResumeUrl = a.ResumeUrl,
                        Firstname = a.Applicant.Firstname,
                        Lastname = a.Applicant.Surname,
                        Email = a.Applicant.UserAccount.Email,
                        JobTitle = a.JobPosting.Title,
                        Type = a.JobPosting.EmploymentType,
                        DateApplied = a.DateApply,
                        TimeInterview = a.Interview.ScheduledTime,
                        DateInterview = a.Interview.ScheduledDate,
                        Status = a.ApplicationStatus
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> Reschedule(int applicationId, string date, string time, string? location)
        {
            // 1️⃣ Get candidate details via navigation properties
            var application = await _context.Application
                .Include(a => a.Applicant)
                    .ThenInclude(a => a.UserAccount)
                .Include(a => a.JobPosting)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null || string.IsNullOrEmpty(application.Applicant?.UserAccount?.Email))
                return false;

            var candidate = new CandidateEmailDto
            {
                Email = application.Applicant.UserAccount.Email,
                Firstname = application.Applicant.Firstname,
                Surname = application.Applicant.Surname,
                JobTitle = application.JobPosting.Title
            };

            var update = await _context.Interview.FirstOrDefaultAsync(i => i.ApplicationId == applicationId);

            if (update is not null)
            {
                if (DateTime.TryParse(time, out var parsedDate))
                    update.ScheduledDate = parsedDate;
                else
                    return false;

                if (TimeSpan.TryParse(date, out var parsedTime))
                    update.ScheduledTime = parsedTime;
                else
                    return false;

                //update.Location = location;
                _context.Interview.Update(update);
                await _context.SaveChangesAsync();
            }

            // 2️⃣ Polished subject and body for rescheduling
            var subject = $"Rescheduled Interview for {candidate.JobTitle} Position";

            var body = $@"Dear {candidate.Firstname},

We hope you are doing well.

We would like to inform you that your interview for the **{candidate.JobTitle}** position has been **rescheduled**.

📅 **New Interview Details**
- **Date:** {date}
- **Time:** {time}
- **Location/Platform:** {location}

We apologize for any inconvenience this change may cause and appreciate your understanding.

Please confirm your availability for the new schedule by replying to this email.

Best regards,  
[Your Name]  
[Your Position]  
[Company Name]";

 
            var result = _emailService.SendEmailAsync(candidate.Email, subject, body);

            return result != null;
        }

        public async Task<int> NoAppearance(int id)
        {
            var response = await _context.Application.FirstOrDefaultAsync(i => i.ApplicationId == id);
            if(response==null)
            {
                return 0;
            }
            response.ApplicationStatus = ApplicationStatus.NoAppearance;
            _context.Update(response);
            return await _context.SaveChangesAsync();

        }


        public async Task<bool> ForTechnical(int applicationId, string date, string time, string? location)
        {
            // 1️⃣ Get candidate details via navigation properties
            var application = await _context.Application
                .Include(a => a.Applicant)
                    .ThenInclude(a => a.UserAccount)
                .Include(a => a.JobPosting)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null || string.IsNullOrEmpty(application.Applicant?.UserAccount?.Email))
                return false;

            var candidate = new CandidateEmailDto
            {
                Email = application.Applicant.UserAccount.Email,
                Firstname = application.Applicant.Firstname,
                Surname = application.Applicant.Surname,
                JobTitle = application.JobPosting.Title
            };

            var updateApplicationStatus = await _context.Application
                .FirstOrDefaultAsync(i => i.ApplicationId == applicationId);

            if (updateApplicationStatus == null)
            {
                return false;
            }

            updateApplicationStatus.ApplicationStatus = ApplicationStatus.Technical;

            _context.Update(updateApplicationStatus);
            await _context.SaveChangesAsync();


            var update = await _context.Interview.FirstOrDefaultAsync(i => i.ApplicationId == applicationId);

            if (update is not null)
            {
                if (DateTime.TryParse(time, out var parsedDate))
                    update.ScheduledDate = parsedDate;
                else
                    return false;

                if (TimeSpan.TryParse(date, out var parsedTime))
                    update.ScheduledTime = parsedTime;
                else
                    return false;

                //update.Location = location;
                _context.Interview.Update(update);
                await _context.SaveChangesAsync();
            }

            // 3️⃣ Professional email content for technical interview
            var subject = $"Invitation for Technical Interview – {candidate.JobTitle}";

            var body = $@"Dear {candidate.Firstname},

Congratulations on progressing to the next stage of your application for the **{candidate.JobTitle}** position.

We are pleased to invite you to a **Technical Interview** to further assess your qualifications and technical skills.

📅 **Interview Details**
- **Date:** {date}
- **Time:** {time}
- **Location/Platform:** {location}

Please confirm your availability by replying to this email at your earliest convenience.

We look forward to meeting you and discussing your experience in greater detail.

Best regards,  
[Your Name]  
[Your Position]  
[Company Name]";


            var result = _emailService.SendEmailAsync(candidate.Email, subject, body);

            return result != null;
        }

        public async Task<bool> ForFinal(int applicationId, string date, string time, string? location)
        {
            // 1️⃣ Get candidate details via navigation properties
            var application = await _context.Application
                .Include(a => a.Applicant)
                    .ThenInclude(a => a.UserAccount)
                .Include(a => a.JobPosting)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null || string.IsNullOrEmpty(application.Applicant?.UserAccount?.Email))
                return false;

            var candidate = new CandidateEmailDto
            {
                Email = application.Applicant.UserAccount.Email,
                Firstname = application.Applicant.Firstname,
                Surname = application.Applicant.Surname,
                JobTitle = application.JobPosting.Title
            };

            var updateApplicationStatus = await _context.Application
                .FirstOrDefaultAsync(i => i.ApplicationId == applicationId);

            if (updateApplicationStatus == null)
            {
                return false;
            }

            updateApplicationStatus.ApplicationStatus = ApplicationStatus.Final;

            _context.Update(updateApplicationStatus);
            await _context.SaveChangesAsync();


            var update = await _context.Interview.FirstOrDefaultAsync(i => i.ApplicationId == applicationId);

            if (update is not null)
            {
                if (DateTime.TryParse(time, out var parsedDate))
                    update.ScheduledDate = parsedDate;
                else
                    return false;

                if (TimeSpan.TryParse(date, out var parsedTime))
                    update.ScheduledTime = parsedTime;
                else
                    return false;

                //update.Location = location;
                _context.Interview.Update(update);
                await _context.SaveChangesAsync();
            }

            // 3️⃣ Professional email content for technical interview
            var subject = $"Invitation for Final Interview – {candidate.JobTitle}";

            var body = $@"Dear {candidate.Firstname},

Congratulations on advancing to the **Final Interview** stage for the **{candidate.JobTitle}** position.

We are pleased to invite you to meet with our hiring panel to discuss your potential fit and next steps within the company.

📅 **Interview Details**
- **Date:** {date}
- **Time:** {time}
- **Location/Platform:** {location}

Please confirm your attendance by replying to this email at your earliest convenience.

We look forward to speaking with you and learning more about your professional goals.

Best regards,  
[Your Name]  
[Your Position]  
[Company Name]";



            var result = _emailService.SendEmailAsync(candidate.Email, subject, body);

            return result != null;
        }

        public async Task<bool> Failed(int applicationId)
        {
            // 1️⃣ Retrieve candidate and related data
            var application = await _context.Application
                .Include(a => a.Applicant)
                    .ThenInclude(a => a.UserAccount)
                .Include(a => a.JobPosting)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null || string.IsNullOrEmpty(application.Applicant?.UserAccount?.Email))
                return false;

            var candidate = new CandidateEmailDto
            {
                Email = application.Applicant.UserAccount.Email,
                Firstname = application.Applicant.Firstname,
                Surname = application.Applicant.Surname,
                JobTitle = application.JobPosting.Title
            };

            // 2️⃣ Update application status
            application.ApplicationStatus = Shared.Enums.ApplicationStatus.Failed;
            _context.Application.Update(application);
            await _context.SaveChangesAsync();

            // 3️⃣ Create professional rejection email content
            var subject = $"Application Update – {candidate.JobTitle} Position";

            var body = $@"Dear {candidate.Firstname},

Thank you very much for your interest in the **{candidate.JobTitle}** position at [Company Name].

After careful consideration, we regret to inform you that you have not been selected to proceed to the next stage of our hiring process.

We sincerely appreciate the time and effort you invested in your application and encourage you to apply for future opportunities that match your skills and experience.

We wish you the very best in your career endeavors.

Best regards,  
[Your Name]  
[Your Position]  
[Company Name]";

            // 4️⃣ Send email
            var result = _emailService.SendEmailAsync(candidate.Email, subject, body);

            return result != null;
        }
        public async Task<bool> DeleteApplication(int id)
        {
            var response = await _context.Application.FirstOrDefaultAsync(i => i.ApplicationId == id);
            if (response == null)
            {
                return false;
            }
            response.IsDeleted = 1;
            _context.Update(response);
            await _context.SaveChangesAsync();
            return true;

        }
        public class CandidateEmailDto
        {
            public string Email { get; set; } = default!;
            public string Firstname { get; set; } = default!;
            public string Surname { get; set; } = default!;
            public string JobTitle { get; set; } = default!;
        }
    }
}
