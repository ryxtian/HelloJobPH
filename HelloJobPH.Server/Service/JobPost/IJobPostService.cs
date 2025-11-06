using HelloJobPH.Employer.Services.JobPosting;
using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.JobPost
{
    public interface IJobPostService
    {
        Task<List<JobPostingDtos>> RetrieveAllAsync();
        Task<JobPostingDtos> GetByIdAsync(int id);
        Task<bool> AddAsync(JobPostingDtos jobPostingDto);
        Task<bool> UpdateAsync(JobPostingDtos jobPostingDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> Activate(int id);
        Task<bool> Deactivate(int id);
    }

}
