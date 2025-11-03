using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.SuperAdmin
{
    public interface IClientSuperAdminService
    {
        Task<List<EmployerListDtos>> EmployerList();

    }
}
