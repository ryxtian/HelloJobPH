using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.Authentication
{
    public interface IAuthService
    {
        Task<LoginDtos> Login(LoginDtos user);
       // Task<IEnumerable<ClaimsDtos>> GetClaims();
    }


}
