
using HelloJobPH.Server.Data;
using HelloJobPH.Server.GeneralReponse;
using HelloJobPH.Server.Service.Email;
using HelloJobPH.Server.Utility;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace HelloJobPH.Server.Service.Candidate
{
    public class CandidateService : ICandidateService
    {
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CandidateService(ApplicationDbContext context, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<GeneralResponse<bool>> CandidateAccepttAsync(int id)
        {
            var userId = Utilities.GetUserId();

            if (userId == null)
            {
                return GeneralResponse<bool>.Fail("Invalid or missing user ID in claims.");
            }

            var hr = await _context.HumanResource
                .FirstOrDefaultAsync(u => u.UserAccountId == userId);

            if (hr == null)
            {
                return GeneralResponse<bool>.Fail("Human Resource not found for the current user.");
            }

            var application = await _context.Application
                .Include(a => a.Applicant)
                .Include(a => a.JobPosting)
                .FirstOrDefaultAsync(i => i.ApplicationId == id);

            if (application == null)
            {
                return GeneralResponse<bool>.Fail("Application not found.");
            }

            application.ApplicationStatus = ApplicationStatus.Accepted;
            application.HumanResourcesId = hr.HumanResourceId;
            _context.Update(application);
            await _context.SaveChangesAsync();

            var auditLog = new Shared.Model.AuditLog
            {
                ApplicationId = application.ApplicationId,
                ApplicantId = application.ApplicantId,
                JobPostingId = application.JobPostingId,
                HumanResourcesId = hr.HumanResourceId,
                EmployerId = hr.EmployerId,
                Action = "Application Accepted",
                Details = $"HR {hr.Firstname} {hr.Lastname} accepted the application of {application.Applicant?.Firstname} {application.Applicant?.Surname} for {application.JobPosting?.Title}.",
                Timestamp = DateTime.UtcNow
            };

            await _context.AuditLog.AddAsync(auditLog);
            await _context.SaveChangesAsync();

         return GeneralResponse<bool>.Ok("Candidate accepted successfully.");
        }

        public async Task<GeneralResponse<bool>> CandidateRejectAsync(int id)
        {
            var userId = Utilities.GetUserId();

            if (userId == null)
            {
                return GeneralResponse<bool>.Fail("Invalid or missing user ID in claims.");
            }

            var hr = await _context.HumanResource
                .FirstOrDefaultAsync(u => u.UserAccountId == userId);

            if (hr == null)
            {
                return GeneralResponse<bool>.Fail("Human Resource not found for the current user.");
            }

            var application = await _context.Application
                .Include(a => a.Applicant)
                .Include(a => a.JobPosting)
                .FirstOrDefaultAsync(i => i.ApplicationId == id);

            if (application == null)
            {
                return GeneralResponse<bool>.Fail("Application not found.");
            }


            application.ApplicationStatus = ApplicationStatus.Rejected;
            application.HumanResourcesId = hr.HumanResourceId;

            _context.Update(application);
            await _context.SaveChangesAsync();

      
            var auditLog = new Shared.Model.AuditLog
            {
                ApplicationId = application.ApplicationId,
                ApplicantId = application.ApplicantId,
                JobPostingId = application.JobPostingId,
                HumanResourcesId = hr.HumanResourceId,
                EmployerId = hr.EmployerId,
                Action = "Application Rejected",
                Details = $"HR {hr.Firstname} {hr.Lastname} rejected the application of {application.Applicant?.Firstname} {application.Applicant?.Surname} for {application.JobPosting?.Title}.",
                Timestamp = DateTime.UtcNow
            };

            await _context.AuditLog.AddAsync(auditLog);
            await _context.SaveChangesAsync();

            return GeneralResponse<bool>.Ok("Candidate rejected successfully.");
        }


        public async Task<List<ApplicationListDtos>> RetrieveAllCandidate()
        {
            var userId = Utilities.GetUserId();
            var hr = await _context.HumanResource
                .FirstOrDefaultAsync(i => i.UserAccountId == userId);

            if(hr is null)
            {
                throw new Exception("Human Resource not found.");
            }

            try
            {
                var result = await _context.Application
                .Where(a => a.ApplicationStatus == ApplicationStatus.Pending || a.ApplicationStatus == ApplicationStatus.Viewed && a.EmployerId ==hr.EmployerId )
                .Select(a => new ApplicationListDtos
                {
                    ApplicationId = a.ApplicationId,
                    Type = a.JobPosting.EmploymentType,
                    Firstname = a.Applicant.Firstname,
                    Lastname = a.Applicant.Surname,
                    Email = a.Applicant.UserAccount.Email,
                    JobTitle = a.JobPosting.Title,
                    DateApplied = a.DateApply,
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
        public async Task<List<ApplicationListDtos>> RetrieveAllAcceptedCandidate()
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
            var result = await _context.Application
                .Where(a => a.ApplicationStatus == ApplicationStatus.Accepted && a.HumanResourcesId == hr.HumanResourceId)
                .Select(a => new ApplicationListDtos
                {
                    ApplicationId = a.ApplicationId,
                    Type = a.JobPosting.EmploymentType,
                    Firstname = a.Applicant.Firstname,
                    Lastname = a.Applicant.Surname,
                    Email = a.Applicant.UserAccount.Email,
                    JobTitle = a.JobPosting.Title,
                    DateApplied = a.DateApply,
                    Status = a.ApplicationStatus
                }).ToListAsync();
            return result;
        }


        public async Task<GeneralResponse<bool>> SendInitialEmail(SetScheduleDto dto)
        {
            var userId = Utilities.GetUserId();

            if (userId == null)
                {
                return GeneralResponse<bool>.Fail("Invalid or missing user ID in claims.");
            }

            var hr = await _context.HumanResource
                .FirstOrDefaultAsync(i=>i.UserAccountId == userId);

            var employer = await _context.Employer.FirstOrDefaultAsync(e => e.EmployerId == hr.EmployerId);


            try
            {
                if (!TimeSpan.TryParse(dto.Time, out var parseTime))
                    return GeneralResponse<bool>.Fail("Invalid time format.");

                if (!DateTime.TryParse(dto.Date, out var parseDate))
                    return GeneralResponse<bool>.Fail("Invalid time format.");

                // Check if interview slot is already taken
                bool slotTaken = await _context.Interview
                    .Where(i=>i.HumanResourceId == hr.HumanResourceId)
                    .AnyAsync(d => d.ScheduledDate == parseDate && d.ScheduledTime == parseTime);

                if (slotTaken)
                   return GeneralResponse<bool>.Fail("The selected interview slot is already taken.");

                var application = await _context.Application
                    .Include(a => a.Applicant)
                        .ThenInclude(a => a.UserAccount)
                    .Include(a => a.JobPosting)
                    .FirstOrDefaultAsync(a => a.ApplicationId == dto.ApplicationId);

                if (application == null || string.IsNullOrEmpty(application.Applicant?.UserAccount?.Email))
                    return GeneralResponse<bool>.Fail("Application or applicant email not found.");

                // Send email
                var candidate = new CandidateEmailDto
                {
                    Email = application.Applicant.UserAccount.Email,
                    Firstname = application.Applicant.Firstname,
                    Surname = application.Applicant.Surname,
                    JobTitle = application.JobPosting.Title
                };

                var subject = $"Initial Interview Invitation for {candidate.JobTitle}";
                var body = $@"Dear {candidate.Firstname},

Thank you for applying for the {candidate.JobTitle} position at {employer.CompanyName}.
You are invited to attend an Initial Interview on {dto.Date} at {dto.Time}.

Location/Platform: {dto.Location}

Please confirm your availability by replying to this email.

Best regards,
[Your Name]
{hr.JobTitle}
{employer.CompanyName}";

                var result = _emailService.SendEmailAsync(candidate.Email, subject, body);




                var interview = new Shared.Model.Interview
                {
                    ScheduledDate = parseDate,
                    ScheduledTime = parseTime,
                    AssignTo = dto.InterviewBy,
                    Mode = dto.Mode,
                    ApplicationId = dto.ApplicationId,
                    HumanResourceId = hr.HumanResourceId,
                };

                await _context.Interview.AddAsync(interview);
                await _context.SaveChangesAsync();


                application.ApplicationStatus = ApplicationStatus.Initial;
                _context.Application.Update(application);
                await _context.SaveChangesAsync();

                var history = new InterviewHistory
                {
                    InterviewBy = dto.InterviewBy,
                    Status = "Scheduled For Initial Interview" , 
                    ApplicationId = application.ApplicationId, 
                    CandidateName = application.Applicant.Firstname+" "+application.Applicant.Surname,
                    Stage = application.ApplicationStatus.ToString(),
                    ScheduledDate = parseDate,
                };
                await _context.InterviewHistory.AddAsync(history);
                await _context.SaveChangesAsync();

                var auditLog = new Shared.Model.AuditLog
                {
                    ApplicationId = application.ApplicationId,
                    ApplicantId = application.ApplicantId,
                    JobPostingId = application.JobPostingId,
                    HumanResourcesId = application.HumanResourcesId,
                    EmployerId = application.EmployerId,
                    Action = "Initial Interview Scheduled",
                    Details = $"Initial interview scheduled for {application.Applicant?.Firstname} {application.Applicant?.Surname} on {dto.Date} at {dto.Time}.",
                    Timestamp = DateTime.UtcNow
                };

                await _context.AuditLog.AddAsync(auditLog);
                await _context.SaveChangesAsync();

                return GeneralResponse<bool>.Ok("Email sent successfully and interview scheduled.", true);
            }
            catch(Exception ex)
            {
                return GeneralResponse<bool>.Fail("Email sent successfully and interview scheduled."+ ex.Message);

            }
        }

        public async Task<GeneralResponse<bool>> ViewResumeUpdate(int id)
        {
            var application = await _context.Application
                .FirstOrDefaultAsync(i=>i.ApplicantId == id);

            if (application is null)
               return GeneralResponse<bool>.Fail("Application not found.");

            application.ApplicationStatus = ApplicationStatus.Viewed;


            _context.Update(application);
            await _context.SaveChangesAsync();

            return GeneralResponse<bool>.Ok("Resume status updated to Viewed.");

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
