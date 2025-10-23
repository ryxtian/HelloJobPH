using HelloJobPH.Employer.Services.JobPosting;
using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.JobPost
{
    public interface IJobPostService
    {
        Task<List<JobPostingDtos>> RetrieveAllAsync();
        Task<JobPostingDtos> GetByIdAsync(int id);
        Task<JobPostingDtos> AddAsync(JobPostingDtos jobPostingDto);
        Task<JobPostingDtos> UpdateAsync(JobPostingDtos jobPostingDto);
        Task<bool> DeleteAsync(int id);
    }

}
