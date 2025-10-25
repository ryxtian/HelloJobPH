
using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
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
            return await _context.Application
                .Include(a => a.Applicant)
                .Include(a => a.JobPosting
                .Select(a => new ApplicationListDtos
                {
                    ApplicationId = a.ApplicationId,
                    Firstname = a.Applicant.Firstname,
                    Email = a.Applicant.Email,
                    JobTitle = a.JobPosting.JobTitle,
                    CompanyName = a.JobPosting.CompanyName,
                    DateApplied = a.DateApplied,
                    Status = a.Status
                })
                .ToListAsync();
        }
    }
}
