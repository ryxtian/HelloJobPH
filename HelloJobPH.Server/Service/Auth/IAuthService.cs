using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using static HelloJobPH.Server.Services.AuthService;

namespace HelloJobPH.Server.Service.Auth
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task<string> LoginAdminAsync(string email, string password);
        //Task LogoutAsync();
        string CreateToken(UserAccount user, HumanResources HRDetails);
    }
}
