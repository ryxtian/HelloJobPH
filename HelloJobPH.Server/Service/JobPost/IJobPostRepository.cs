using HelloJobPH.Shared.Model;

namespace HelloJobPH.Server.Service.JobPost
{
    public interface IJobPostRepository
    {
        Task AddAsync(JobPosting jobPost);
        Task<JobPosting?> GetByIdAsync(int id);
        Task<List<JobPosting>> RetrieveAllAsync();
        void Update(JobPosting jobPost);
    }
}
