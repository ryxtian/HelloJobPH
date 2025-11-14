using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.HumanResource
{
    public interface IHumanResource
    {
        Task<bool> AddAsync(HumanResourceDtos hr);
        Task<bool> SoftDeleteJobPost(int id);
        Task<HumanResourceDtos> GetSingle(int id);
        Task<List<HumanResourceDtos>> RetrieveAllAsync();
        Task<string> UpdateAsync(HumanResourceDtos hr);
    }
}
