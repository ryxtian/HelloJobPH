using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;

namespace HelloJobPH.Server.Service.Auth
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task LogoutAsync();
        string CreateToken(UserAccount user, HumanResources HRDetails);
    }
}
