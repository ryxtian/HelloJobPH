using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.HumanResource
{
    public interface IHumanResourceService
    {
        Task<List<HumanResourceDtos>> RetrieveAllAsync();
        Task<HumanResourceDtos> GetByIdAsync(int id);
        Task<bool> AddAsync(HumanResourceDtos jobPostingDto);
        Task<bool> UpdateAsync(HumanResourceDtos jobPostingDto);
        Task<bool> DeleteAsync(int id);
    }
}
