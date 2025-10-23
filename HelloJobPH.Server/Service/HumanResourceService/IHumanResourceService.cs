using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.HumanResource
{
    public interface IHumanResourceService
    {
        Task<List<HumanResourceDtos>> RetrieveAllAsync();
        Task<HumanResourceDtos> GetByIdAsync(int id);
        Task<HumanResourceDtos> AddAsync(HumanResourceDtos jobPostingDto);
        Task<HumanResourceDtos> UpdateAsync(HumanResourceDtos jobPostingDto);
        Task<bool> DeleteAsync(int id);
    }
}
