using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.JobPosting
{
    public interface IJobPosting
    {
        Task<string> AddAsync(JobPostingDtos jobPost);
        Task<bool> SoftDeleteJobPost(int id);
        Task<JobPostingDtos> GetSingleJobPost(int id);
        Task<List<JobPostingDtos>> RetrieveAllAsync();
        Task<string> UpdateAsync(JobPostingDtos jobpost);
    }
}
