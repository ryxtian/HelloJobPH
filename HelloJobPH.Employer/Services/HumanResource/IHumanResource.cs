using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.HumanResource
{
    public interface IHumanResource
    {
        Task<string> AddAsync(HumanResourceDtos jobPost);
        Task<bool> SoftDeleteJobPost(int id);
        Task<JobPostingDtos> GetSingle(int id);
        Task<List<HumanResourceDtos>> RetrieveAllAsync();
        Task<string> UpdateAsync(JobPostingDtos jobpost);
    }
}
