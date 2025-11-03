using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.SuperAdmin
{
    public interface ISuperAdminService
    {
        Task<List<EmployerListDtos>> EmployersList();
    }
}
