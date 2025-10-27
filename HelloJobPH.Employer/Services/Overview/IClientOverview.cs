using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.Overview
{
    public interface IClientOverview
    {
        Task<OverviewDtos?> OverviewApplication(int id);
    }
}
