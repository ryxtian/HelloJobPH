
using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.Candidate
{
    public class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext _context;
        public CandidateService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<bool> CandidateAccepttAsync(int id)
        {
            throw new NotImplementedException();
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
                where app.ApplicationStatus == ApplicationStatus.Initial
                select new ApplicationListDtos
                {
                    ApplicationId = app.ApplicationId,
                    ResumeUrl = app.ResumeUrl,
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

    }
}
