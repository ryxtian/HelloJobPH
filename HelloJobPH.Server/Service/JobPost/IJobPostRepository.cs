using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;

namespace HelloJobPH.Server.Service.JobPost
{
    public interface IJobPostRepository
    {
        Task<List<JobPosting>> RetrieveAllAsync();
        Task<JobPosting> GetByIdAsync(int id);
        Task<JobPosting> AddAsync(JobPosting jobPosting);
        Task<JobPosting> UpdateAsync(JobPosting jobPosting);
        Task<bool> DeleteAsync(int id);
    }
}
