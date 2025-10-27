
using HelloJobPH.Server.Data;
using HelloJobPH.Server.Service.Email;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.Candidate
{
    public class CandidateService : ICandidateService
    {
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        public CandidateService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<bool> CandidateAccepttAsync(int id)
        {
            var response = await _context.Application.FirstOrDefaultAsync(i => i.ApplicationId == id);
            if(response ==null)
            {
                throw new Exception("Application not found.");
            }
            response.ApplicationStatus = ApplicationStatus.Accepted;
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
            var result = await (
                from app in _context.Application
                join applicant in _context.Applicant on app.ApplicantId equals applicant.ApplicantId
                join user in _context.UserAccount on applicant.UserAccountId equals user.UserAccountId
                join job in _context.JobPosting on app.JobPostId equals job.JobPostingId
                where app.ApplicationStatus == ApplicationStatus.Pending
                select new ApplicationListDtos
                {
                    ApplicationId = app.ApplicationId,
                    Type = job.EmploymentType,
                    Firstname = applicant.Firstname,
                    Lastname = applicant.Surname,
                    Email = user.Email,
                    JobTitle = job.Title,
                    //CompanyName = job.CompanyName,
                    DateApplied = app.DateApply,
                    Status = app.ApplicationStatus
                }
            ).ToListAsync();

            return result;
        }
        public async Task<List<ApplicationListDtos>> RetrieveAllAcceptedCandidate()
        {
            var result = await (
                from app in _context.Application
                join applicant in _context.Applicant on app.ApplicantId equals applicant.ApplicantId
                join user in _context.UserAccount on applicant.UserAccountId equals user.UserAccountId
                join job in _context.JobPosting on app.JobPostId equals job.JobPostingId
                where app.ApplicationStatus == ApplicationStatus.Accepted
                select new ApplicationListDtos
                {
                    ApplicationId = app.ApplicationId,
                    Type = job.EmploymentType,
                    Firstname = applicant.Firstname,
                    Lastname = applicant.Surname,
                    Email = user.Email,
                    JobTitle = job.Title,
                    //CompanyName = job.CompanyName,
                    DateApplied = app.DateApply,
                    Status = app.ApplicationStatus
                }
            ).ToListAsync();

            return result;
        }


        public async Task<bool> SendInitialEmail(int applicationId, string date, string time, string? location)
        {
            // 1️⃣ Get candidate details
            var candidate = await (from user in _context.UserAccount
                                   join applicant in _context.Applicant on user.UserAccountId equals applicant.UserAccountId
                                   join application in _context.Application on applicant.ApplicantId equals application.ApplicantId
                                   join job in _context.JobPosting on application.JobPostId equals job.JobPostingId
                                   where application.ApplicationId == applicationId
                                   select new CandidateEmailDto
                                   {
                                       Email = user.Email,
                                       Firstname = applicant.Firstname,
                                       Surname = applicant.Surname,
                                       JobTitle = job.Title
                                   }).FirstOrDefaultAsync();

            if (candidate == null || string.IsNullOrEmpty(candidate.Email))
                return false;

            // 2️⃣ Create email content
            var subject = $"Initial Interview Invitation for {candidate.JobTitle}";
            var body = $@"Dear {candidate.Firstname},

Thank you for applying for the {candidate.JobTitle} position at [Company Name].
You are invited to attend an Initial Interview on {time:MMMM dd, yyyy} at {date:hh\\:mm}.

Location/Platform: {location}

Please confirm your availability by replying to this email.

Best regards,
[Your Name]
[Your Position]
[Company Name]";

            // 3️⃣ Send email using injected service
            var result = _emailService.SendEmailAsync(candidate.Email, subject, body);
            if(result is null)
            {
                return false;
            }
            return true;
        }

        // DTO to avoid anonymous type issues
        public class CandidateEmailDto
        {
            public string Email { get; set; } = default!;
            public string Firstname { get; set; } = default!;
            public string Surname { get; set; } = default!;
            public string JobTitle { get; set; } = default!;
        }

    }
}
