
using HelloJobPH.Server.Data;
using HelloJobPH.Server.Service.Email;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelloJobPH.Server.Service.Candidate
{
    public class CandidateService : ICandidateService
    {
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        IHttpContextAccessor _httpContextAccessor;
        public CandidateService(ApplicationDbContext context, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CandidateAccepttAsync(int id)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            // ✅ Get the user’s ID from claims
            var userIdClaim = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
                throw new Exception("Invalid or missing user ID in claims.");
            var response = await _context.Application.FirstOrDefaultAsync(i => i.ApplicationId == id);
            if(response ==null)
            {
                throw new Exception("Application not found.");
            }
            response.ApplicationStatus = ApplicationStatus.Accepted;
            response.HumanResourceId = userId;
             _context.Update(response);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> CandidateRejectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ApplicationListDtos>> RetrieveAllCandidate()
        {
            var result = await _context.Application
                .Where(a => a.ApplicationStatus == ApplicationStatus.Pending)
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
        

            //var result = await (
            //    from app in _context.Application
            //    join applicant in _context.Applicant on app.ApplicantId equals applicant.ApplicantId
            //    join user in _context.UserAccount on applicant.UserAccountId equals user.UserAccountId
            //    join job in _context.JobPosting on app.JobPostId equals job.JobPostingId
            //    where app.ApplicationStatus == ApplicationStatus.Pending
            //    select new ApplicationListDtos
            //    {
            //        ApplicationId = app.ApplicationId,
            //        Type = job.EmploymentType,
            //        Firstname = applicant.Firstname,
            //        Lastname = applicant.Surname,
            //        Email = user.Email,
            //        JobTitle = job.Title,
            //        //CompanyName = job.CompanyName,
            //        DateApplied = app.DateApply,
            //        Status = app.ApplicationStatus
            //    }
            //).ToListAsync();

            //return result;
        }
        public async Task<List<ApplicationListDtos>> RetrieveAllAcceptedCandidate()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            // ✅ Get the user’s ID from claims
            var userIdClaim = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
                throw new Exception("Invalid or missing user ID in claims.");
            var result = await _context.Application
                .Where(a => a.ApplicationStatus == ApplicationStatus.Accepted && a.HumanResourceId == userId)
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


        public async Task<bool> SendInitialEmail(int applicationId, string date, string time, string? location)
        {
            if(!TimeSpan.TryParse(date, out var parseTime))
            {
                return false;
            }

            bool isTaken = await _context.Interview.AnyAsync(d => d.ScheduledTime == parseTime && d.ScheduledDate.ToString() == time);

            if(!isTaken)
            {
                return false;
            }

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

            var subject = $"Initial Interview Invitation for {candidate.JobTitle}";
            var body = $@"Dear {candidate.Firstname},

Thank you for applying for the {candidate.JobTitle} position at [Company Name].
You are invited to attend an Initial Interview on {date} at {time}.

Location/Platform: {location}

Please confirm your availability by replying to this email.

Best regards,
[Your Name]
[Your Position]
[Company Name]";

            var result = _emailService.SendEmailAsync(candidate.Email, subject, body);

            return result != null;
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
