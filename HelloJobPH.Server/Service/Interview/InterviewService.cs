using HelloJobPH.Server.Data;
using HelloJobPH.Server.GeneralReponse;
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

            if (userId == null)
            {
                throw new Exception("Invalid or missing user ID in claims.");
            }
            var hr = await _context.HumanResource.FirstOrDefaultAsync
            (u => u.UserAccountId == userId);

            if (hr == null)
            {
                throw new Exception("Invalid or missing user ID in claims.");
            }
            try
            {
                var result = await _context.Application
                    .Where(a => a.ApplicationStatus == ApplicationStatus.Final && a.IsDeleted == 0 && a.HumanResourcesId == hr.HumanResourceId)
                    .Select(a => new InterviewListDtos
                    {
                        ApplicationId = a.ApplicationId,
                        Firstname = a.Applicant.Firstname,
                        Lastname = a.Applicant.Surname,
                        Email = a.Applicant.UserAccount.Email,
                        JobTitle = a.JobPosting.Title,
                        Type = a.JobPosting.EmploymentType,
                        DateApplied = a.DateApply,
                        TimeInterview = a.Interview.ScheduledTime,
                        DateInterview = a.Interview.ScheduledDate,
                        Status = a.ApplicationStatus,
                        MarkAsCompleted = a.MarkAsCompleted
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
            var datetoday = DateTime.Now;



            var userId = Utilities.GetUserId();

            // Try to find HR or Employer linked to this user
            var hr = await _context.HumanResource.FirstOrDefaultAsync(i => i.UserAccountId == userId);
            var employer = await _context.Employer.FirstOrDefaultAsync(i => i.UserAccountId == userId);

            var employerId = hr?.EmployerId ?? employer?.EmployerId;

            if (employerId == null)
                throw new Exception("No associated employer found for this user.");
            try
                {
                var validStatuses = new[]
                {
    ApplicationStatus.Initial,
    ApplicationStatus.Technical,
    ApplicationStatus.Final
};

                var result = await _context.Application
                    .Where(a => validStatuses.Contains(a.ApplicationStatus)
                                && a.IsDeleted == 0
                                && a.EmployerId == employerId
                                && a.HumanResourcesId == hr.HumanResourceId
                                && a.Interview.ScheduledDate >= datetoday)
                    .Select(a => new InterviewListDtos
                    {
                        ApplicationId = a.ApplicationId,
                        ApplicantId = a.ApplicantId,
                        Firstname = a.Applicant.Firstname,
                        Lastname = a.Applicant.Surname,
                        Email = a.Applicant.UserAccount.Email,
                        AssignTo = a.Interview.Interviewer.Name,
                        JobTitle = a.JobPosting.Title,
                        Type = a.JobPosting.EmploymentType,
                        DateApplied = a.DateApply,
                        TimeInterview = a.Interview != null ? a.Interview.ScheduledTime : null,
                        DateInterview = a.Interview != null ? a.Interview.ScheduledDate : null,
                        Status = a.ApplicationStatus,
                        MarkAsCompleted = a.MarkAsCompleted,
                        UserAccountId = a.Applicant.UserAccountId
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

            if (userId == null)
            {
                throw new Exception("Invalid or missing user ID in claims.");
            }
            var hr = await _context.HumanResource.FirstOrDefaultAsync
            (u => u.UserAccountId == userId);

            if (hr == null)
            {
                throw new Exception("Invalid or missing user ID in claims.");
            }
            try
            {
                var result = await _context.Application
                    .Where(a => a.ApplicationStatus == ApplicationStatus.Technical && a.IsDeleted == 0 && a.HumanResourcesId == hr.HumanResourceId)
                    .Select(a => new InterviewListDtos
                    {
                        ApplicationId = a.ApplicationId,
                        Firstname = a.Applicant.Firstname,
                        Lastname = a.Applicant.Surname,
                        Email = a.Applicant.UserAccount.Email,
                        JobTitle = a.JobPosting.Title,
                        Type = a.JobPosting.EmploymentType,
                        DateApplied = a.DateApply,
                        TimeInterview = a.Interview != null ? a.Interview.ScheduledTime : null,
                        DateInterview = a.Interview != null ? a.Interview.ScheduledDate : null,
                        Status = a.ApplicationStatus,
                        MarkAsCompleted = a.MarkAsCompleted
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GeneralResponse<bool>> Reschedule(SetScheduleDto dto)
        {



            var application = await _context.Application
                .Include(a => a.Applicant)
                    .ThenInclude(a => a.UserAccount)
                .Include(a => a.JobPosting)
                .Include(a => a.HumanResources) // Include HR if needed for audit
                .FirstOrDefaultAsync(a => a.ApplicationId == dto.ApplicationId);



            if (application == null || string.IsNullOrEmpty(application.Applicant?.UserAccount?.Email))
                return GeneralResponse<bool>.Fail("Application not found or missing candidate email.");



            var hr = application.HumanResources;
            if (hr == null)
                return GeneralResponse<bool>.Fail("Not Found HR"); // Or handle the missing HR case


            var employer = await _context.Employer.FirstOrDefaultAsync(e => e.EmployerId == hr.EmployerId);

            if (!DateTime.TryParse(dto.Date, out var parsedDate))
                return GeneralResponse<bool>.Fail("Invalid format");

            if (!TimeSpan.TryParse(dto.Time, out var parsedTime))
                return GeneralResponse<bool>.Fail("Invalid format");

            var interviewer = await _context.Interviewer
    .FirstOrDefaultAsync(i => i.InterviewerId == dto.InterviewerId);
            bool slotTaken = await _context.Interview
                .Where(i => i.InterviewerId == interviewer.InterviewerId)
                .AnyAsync(d => d.ScheduledDate == parsedDate && d.ScheduledTime == parsedTime);

            if (slotTaken)
                return GeneralResponse<bool>.Fail("The selected interview slot is already taken.");


            // 2️⃣ Update application status
            application.MarkAsCompleted = 0;
            _context.Application.Update(application);
            await _context.SaveChangesAsync();


            // 3️⃣ Update interview
            var interview = await _context.Interview.FirstOrDefaultAsync(i => i.ApplicationId == dto.ApplicationId);
            if (interview != null)
            {
                interview.ScheduledDate = parsedDate;
                interview.ScheduledTime = parsedTime;
                interview.InterviewerId = dto.InterviewerId;
                interview.Mode = dto.Mode;

                _context.Interview.Update(interview);
                await _context.SaveChangesAsync();
            }

            // 4️⃣ Add audit log for reschedule
            var auditLog = new Shared.Model.AuditLog
            {
                ApplicationId = application.ApplicationId,
                ApplicantId = application.ApplicantId,
                JobPostingId = application.JobPostingId,
                HumanResourcesId = hr.HumanResourceId,
                EmployerId = hr.EmployerId,
                Action = "Interview Rescheduled",
                Details = $"HR {hr.Firstname} {hr.Lastname} rescheduled the interview for {application.Applicant?.Firstname} {application.Applicant?.Surname} for {application.JobPosting?.Title} to {parsedDate:yyyy-MM-dd} at {parsedTime}.",
                Timestamp = DateTime.UtcNow
            };

            await _context.AuditLog.AddAsync(auditLog);
            await _context.SaveChangesAsync();


            var history = new InterviewHistory
            {
                InterviewerId = dto.InterviewerId,
                Status = "Reschedule Interview",
                ApplicationId = application.ApplicationId,
                CandidateName = application.Applicant.Firstname + " " + application.Applicant.Surname,
                Stage = application.ApplicationStatus.ToString(),
                ScheduledDate = parsedDate,
            };
            await _context.InterviewHistory.AddAsync(history);
            await _context.SaveChangesAsync();
            // 5️⃣ Prepare email
            var candidate = application.Applicant;
            var subject = $"Rescheduled Interview for {application.JobPosting?.Title} Position";
            var body = $@"Dear {candidate?.Firstname},

We hope you are doing well.

We would like to inform you that your interview for the **{application.JobPosting?.Title}** position has been **rescheduled**.

📅 **New Interview Details**
- **Date:** {parsedDate:yyyy-MM-dd}
- **Time:** {parsedTime}
- **Location/Platform:** {dto.Location ?? dto.Mode}

We apologize for any inconvenience this change may cause and appreciate your understanding.

Please confirm your availability for the new schedule by replying to this email.

Best regards,  
{hr.Firstname}  
{hr.JobTitle} 
{employer.CompanyName}";

            var result = _emailService.SendEmailAsync(candidate.UserAccount.Email, subject, body);



            return GeneralResponse<bool>.Ok("Successfully rechedule");
        }


        public async Task<GeneralResponse<int>> NoAppearance(int applicationId)
        {
            // 1️⃣ Get application with related entities
            var application = await _context.Application
                .Include(a => a.Applicant)
                .Include(a => a.JobPosting)
                .Include(a => a.HumanResources) // HR assigned to the application
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null)
                return GeneralResponse<int>.Fail("No application found");

            // 2️⃣ Update application status
            application.ApplicationStatus = ApplicationStatus.NoAppearance;
            application.MarkAsCompleted = 0;
            _context.Application.Update(application);
            var result = await _context.SaveChangesAsync();

            // 3️⃣ Create audit log
            var hr = application.HumanResources;

            var auditLog = new Shared.Model.AuditLog
            {
                ApplicationId = application.ApplicationId,
                ApplicantId = application.ApplicantId,
                JobPostingId = application.JobPostingId,
                HumanResourcesId = hr?.HumanResourceId,
                EmployerId = hr?.EmployerId,
                Action = "No-Show / No Appearance",
                Details = $"Application of {application.Applicant?.Firstname} {application.Applicant?.Surname} for {application.JobPosting?.Title} marked as No Appearance.",
                Timestamp = DateTime.UtcNow
            };

            await _context.AuditLog.AddAsync(auditLog);
            await _context.SaveChangesAsync();

            return GeneralResponse<int>.Ok("Mark as No appearance");
        }



        public async Task<GeneralResponse<bool>> ForTechnical(SetScheduleDto dto)
        {
            // 1️⃣ Get application with related entities
            var application = await _context.Application
                .Include(a => a.Applicant)
                    .ThenInclude(a => a.UserAccount)
                .Include(a => a.JobPosting)
                .Include(a => a.HumanResources) // Include HR for audit
                .FirstOrDefaultAsync(a => a.ApplicationId == dto.ApplicationId);


            if (!DateTime.TryParse(dto.Date, out var parsedDate))
                return GeneralResponse<bool>.Fail("Invalid format");

            if (!TimeSpan.TryParse(dto.Time, out var parsedTime))
                return GeneralResponse<bool>.Fail("Invalid format");


            var interviewer = await _context.Interviewer
  .FirstOrDefaultAsync(i => i.InterviewerId == dto.InterviewerId);
            bool slotTaken = await _context.Interview
                .Where(i => i.InterviewerId == interviewer.InterviewerId)
                .AnyAsync(d => d.ScheduledDate == parsedDate && d.ScheduledTime == parsedTime);

            if (slotTaken)
                return GeneralResponse<bool>.Fail("The selected interview slot is already taken.");


            if (application == null || string.IsNullOrEmpty(application.Applicant?.UserAccount?.Email))
                return GeneralResponse<bool>.Fail("no application found");

            var hr = application.HumanResources;

            var employer = await _context.Employer
                .FirstOrDefaultAsync(i => i.EmployerId == hr.EmployerId);

            var candidate = new CandidateEmailDto
            {
                Email = application.Applicant.UserAccount.Email,
                Firstname = application.Applicant.Firstname,
                Surname = application.Applicant.Surname,
                JobTitle = application.JobPosting.Title
            };


            // 5️⃣ Send email
            var subject = $"Invitation for Technical Interview – {candidate.JobTitle}";
            var body = $@"Dear {candidate.Firstname},

Congratulations on progressing to the next stage of your application for the **{candidate.JobTitle}** position.

We are pleased to invite you to a **Technical Interview** to further assess your qualifications and technical skills.

📅 **Interview Details**
- **Date:** {dto.Date}
- **Time:** {dto.Time}
- **Location/Platform:** {dto.Location ?? dto.Mode}

Please confirm your availability by replying to this email at your earliest convenience.

We look forward to meeting you and discussing your experience in greater detail.

Best regards,  
{hr.Firstname}  
{hr.JobTitle}
{employer.CompanyName}";

            var result = _emailService.SendEmailAsync(candidate.Email, subject, body);


            
            application.ApplicationStatus = ApplicationStatus.Technical;
            application.MarkAsCompleted = 0;
            _context.Application.Update(application);
            await _context.SaveChangesAsync();

            // 3️⃣ Update interview schedule
            var interview = await _context.Interview.FirstOrDefaultAsync(i => i.ApplicationId == dto.ApplicationId);


            interview.InterviewerId = dto.InterviewerId;
            interview.ScheduledDate = parsedDate;
                interview.ScheduledTime = parsedTime;
            interview.Mode = dto.Mode;

                _context.Interview.Update(interview);
                await _context.SaveChangesAsync();
            

            // 4️⃣ Add audit log
            var auditLog = new Shared.Model.AuditLog
            {
                ApplicationId = application.ApplicationId,
                ApplicantId = application.ApplicantId,
                JobPostingId = application.JobPostingId,
                HumanResourcesId = hr?.HumanResourceId,
                EmployerId = hr?.EmployerId,
                Action = "Technical Interview Scheduled",
                Details = $"HR {hr?.Firstname} {hr?.Lastname} scheduled a technical interview for {application.Applicant?.Firstname} {application.Applicant?.Surname} for {application.JobPosting?.Title} on {dto.Date} at {dto.Time}.",
                Timestamp = DateTime.UtcNow
            };

            await _context.AuditLog.AddAsync(auditLog);
            await _context.SaveChangesAsync();

            var history = new InterviewHistory
            {
                InterviewerId = dto.InterviewerId,
                Status = "Schedule for Technical interview",
                ApplicationId = application.ApplicationId,
                CandidateName = application.Applicant.Firstname + " " + application.Applicant.Surname,
                Stage = application.ApplicationStatus.ToString(),
                ScheduledDate = parsedDate,
            };
            await _context.InterviewHistory.AddAsync(history);
            await _context.SaveChangesAsync();


            return GeneralResponse<bool>.Ok("successfully scheduled for techincal interview");
        }


        public async Task<GeneralResponse<bool>> ForFinal(SetScheduleDto dto)
        {
            // 1️⃣ Get application with related entities
            var application = await _context.Application
                .Include(a => a.Applicant)
                    .ThenInclude(a => a.UserAccount)
                .Include(a => a.JobPosting)
                .Include(a => a.HumanResources) // Include HR for audit logging
                .FirstOrDefaultAsync(a => a.ApplicationId == dto.ApplicationId);

            if (application == null || string.IsNullOrEmpty(application.Applicant?.UserAccount?.Email))
                return GeneralResponse<bool>.Fail("applicaiton not found");






            if (!DateTime.TryParse(dto.Date, out var parsedDate))
                return GeneralResponse<bool>.Fail("Invalid format");

            if (!TimeSpan.TryParse(dto.Time, out var parsedTime))
                return GeneralResponse<bool>.Fail("Invalid format");


            var interviewer = await _context.Interviewer
  .FirstOrDefaultAsync(i => i.InterviewerId == dto.InterviewerId);
            bool slotTaken = await _context.Interview
                .Where(i => i.InterviewerId == interviewer.InterviewerId)
                .AnyAsync(d => d.ScheduledDate == parsedDate && d.ScheduledTime == parsedTime);

            if (slotTaken)
                return GeneralResponse<bool>.Fail("The selected interview slot is already taken.");



            var hr = application.HumanResources;

            var employer = await _context.Employer
                .FirstOrDefaultAsync(i => i.EmployerId == hr.EmployerId);

            var candidate = new CandidateEmailDto
            {
                Email = application.Applicant.UserAccount.Email,
                Firstname = application.Applicant.Firstname,
                Surname = application.Applicant.Surname,
                JobTitle = application.JobPosting.Title
            };

            // 2️⃣ Update application status
            application.ApplicationStatus = ApplicationStatus.Final;
            application.MarkAsCompleted = 0;
            _context.Application.Update(application);
            await _context.SaveChangesAsync();
 

            // 3️⃣ Update interview schedule
            var interview = await _context.Interview.FirstOrDefaultAsync(i => i.ApplicationId == dto.ApplicationId);



            interview.InterviewerId = dto.InterviewerId;
            interview.ScheduledDate = parsedDate;
            interview.ScheduledTime = parsedTime;
            interview.Mode = dto.Mode;

            //interview.Location = location;
            _context.Interview.Update(interview);
                await _context.SaveChangesAsync();
            


            // 5️⃣ Send email
            var subject = $"Invitation for Final Interview – {candidate.JobTitle}";
            var body = $@"Dear {candidate.Firstname},

Congratulations on advancing to the **Final Interview** stage for the **{candidate.JobTitle}** position.

We are pleased to invite you to meet with our hiring panel to discuss your potential fit and next steps within the company.

📅 **Interview Details**
- **Date:** {dto.Date}
- **Time:** {dto.Time}
- **Location/Platform:** {dto.Location ?? dto.Mode}

Please confirm your attendance by replying to this email at your earliest convenience.

We look forward to speaking with you and learning more about your professional goals.

Best regards,  
{hr.Firstname} 
{hr.JobTitle}
{employer.CompanyName}";

            var result = _emailService.SendEmailAsync(candidate.Email, subject, body);


            // 4️⃣ Add audit log
            var auditLog = new Shared.Model.AuditLog
            {
                ApplicationId = application.ApplicationId,
                ApplicantId = application.ApplicantId,
                JobPostingId = application.JobPostingId,
                HumanResourcesId = hr?.HumanResourceId,
                EmployerId = hr?.EmployerId,
                Action = "Final Interview Scheduled",
                Details = $"HR {hr?.Firstname} {hr?.Lastname} scheduled a final interview for {application.Applicant?.Firstname} {application.Applicant?.Surname} for {application.JobPosting?.Title} on {dto.Date} at {dto.Time}.",
                Timestamp = DateTime.UtcNow
            };

            await _context.AuditLog.AddAsync(auditLog);
            await _context.SaveChangesAsync();

            var history = new InterviewHistory
            {
                InterviewerId = dto.InterviewerId,
                Status = "Schedule for Final interview",
                ApplicationId = application.ApplicationId,
                CandidateName = application.Applicant.Firstname + " " + application.Applicant.Surname,
                Stage = application.ApplicationStatus.ToString(),
                ScheduledDate = parsedDate,
            };
            await _context.InterviewHistory.AddAsync(history);
            await _context.SaveChangesAsync();


            return GeneralResponse<bool>.Ok("successfully scheduled for final interview");
        }


        public async Task<GeneralResponse<bool>> Failed(int applicationId)
        {
            // 1️⃣ Retrieve candidate and related data
            var application = await _context.Application
                .Include(a => a.Applicant)
                    .ThenInclude(a => a.UserAccount)
                .Include(a => a.JobPosting)
                .Include(a => a.HumanResources) // Include HR for audit
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null || string.IsNullOrEmpty(application.Applicant?.UserAccount?.Email))
                return GeneralResponse<bool>.Fail("applicaiton not found");

            var hr = application.HumanResources;

            var employer = await _context.Employer
              .FirstOrDefaultAsync(i => i.EmployerId == hr.EmployerId);

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

            // 3️⃣ Add audit log
            var auditLog = new Shared.Model.AuditLog
            {
                ApplicationId = application.ApplicationId,
                ApplicantId = application.ApplicantId,
                JobPostingId = application.JobPostingId,
                HumanResourcesId = hr?.HumanResourceId,
                EmployerId = hr?.EmployerId,
                Action = "Application Failed",
                Details = $"HR {hr?.Firstname} {hr?.Lastname} marked the application of {application.Applicant?.Firstname} {application.Applicant?.Surname} for {application.JobPosting?.Title} as Failed.",
                Timestamp = DateTime.UtcNow
            };

            await _context.AuditLog.AddAsync(auditLog);
            await _context.SaveChangesAsync();


            // 4️⃣ Send rejection email
            var subject = $"Application Update – {candidate.JobTitle} Position";

            var body = $@"Dear {candidate.Firstname},

Thank you very much for your interest in the **{candidate.JobTitle}** position at {employer.CompanyName}.

After careful consideration, we regret to inform you that you have not been selected to proceed to the next stage of our hiring process.

We sincerely appreciate the time and effort you invested in your application and encourage you to apply for future opportunities that match your skills and experience.

We wish you the very best in your career endeavors.

Best regards,  
{hr.Firstname}  
{hr.JobTitle}
{employer.CompanyName}";

            var result = _emailService.SendEmailAsync(candidate.Email, subject, body);

            return GeneralResponse<bool>.Ok("successfully mark as failed");

        }

        public async Task<GeneralResponse<bool>> DeleteApplication(int id)
        {
            var application = await _context.Application
                .Include(a => a.Applicant)
                .Include(a => a.JobPosting)
                .Include(a => a.HumanResources)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (application == null)
                return GeneralResponse<bool>.Fail("Applicaiton not found");

            // 1️⃣ Mark as deleted
            application.IsDeleted = 1;
            _context.Application.Update(application);
            await _context.SaveChangesAsync();

            // 2️⃣ Add audit log
            var hr = application.HumanResources;
            var auditLog = new Shared.Model.AuditLog
            {
                ApplicationId = application.ApplicationId,
                ApplicantId = application.ApplicantId,
                JobPostingId = application.JobPostingId,
                HumanResourcesId = hr?.HumanResourceId,
                EmployerId = hr?.EmployerId,
                Action = "Application Deleted",
                Details = $"HR {hr?.Firstname} {hr?.Lastname} marked the application of {application.Applicant?.Firstname} {application.Applicant?.Surname} for {application.JobPosting?.Title} as deleted.",
                Timestamp = DateTime.UtcNow
            };

            await _context.AuditLog.AddAsync(auditLog);
            await _context.SaveChangesAsync();

            return GeneralResponse<bool>.Ok("Successfully deleted");
        }

        public async Task<GeneralResponse<bool>> MarkAsCompleted(int id)
        {
            var application = await _context.Application
                .Include(a => a.Applicant)
                .Include(a => a.JobPosting)
                .Include(a => a.HumanResources)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (application == null)
                return GeneralResponse<bool>.Fail("Applicaiton not found");

            // 1️⃣ Mark as deleted
            application.MarkAsCompleted = 1;
            _context.Application.Update(application);
            await _context.SaveChangesAsync();

            //// 2️⃣ Add audit log
            //var hr = application.HumanResources;
            //var auditLog = new Shared.Model.AuditLog
            //{
            //    ApplicationId = application.ApplicationId,
            //    ApplicantId = application.ApplicantId,
            //    JobPostingId = application.JobPostingId,
            //    HumanResourcesId = hr?.HumanResourceId,
            //    EmployerId = hr?.EmployerId,
            //    Action = "Application Deleted",
            //    Details = $"HR {hr?.Firstname} {hr?.Lastname} marked the application of {application.Applicant?.Firstname} {application.Applicant?.Surname} for {application.JobPosting?.Title} as deleted.",
            //    Timestamp = DateTime.UtcNow
            //};

            //await _context.AuditLog.AddAsync(auditLog);
            //await _context.SaveChangesAsync();

            return GeneralResponse<bool>.Ok("Successfully mark as completed");
        }

        public async Task<List<InterviewerDtos>> InterviewerList()
        {
            var interviewers = await  _context.Interviewer
                //.Where(i => i.IsDeleted == 0)
                .Select(i => new InterviewerDtos
                {
                    InterviewerId = i.InterviewerId,
                    Name = i.Name,
                   
                })
                .ToListAsync();
            return interviewers;
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
