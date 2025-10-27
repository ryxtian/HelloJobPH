using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.Overview
{
    public interface IOverviewService
    {
        Task<OverviewDtos?> ListOverview(int id);
    }
}
