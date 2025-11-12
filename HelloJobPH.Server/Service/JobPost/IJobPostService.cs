using HelloJobPH.Employer.Services.JobPosting;
using HelloJobPH.Server.GeneralReponse;
using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.JobPost
{
    public interface IJobPostService
    {
        Task<List<JobPostingDtos>> RetrieveAllAsync();
        Task<GeneralResponse<JobPostingDtos>> GetByIdAsync(int id);
        Task<GeneralResponse<JobPostingDtos>> AddAsync(JobPostingDtos jobPostingDto);
        Task<GeneralResponse<JobPostingDtos>> UpdateAsync(JobPostingDtos jobPostingDto);
        Task<GeneralResponse<bool>> DeleteAsync(int id);
        Task<GeneralResponse<bool>> Activate(int id);
        Task<GeneralResponse<bool>> Deactivate(int id);
    }
}
