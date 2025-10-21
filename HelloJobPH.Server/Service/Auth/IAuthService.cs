using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;

namespace HelloJobPH.Server.Service.Auth
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task LogoutAsync();
        string CreateToken(UserAccount user, Applicant applicantdetail);
        Task<string?> GetToken();
        Task<string> RegisterAsync(RegisterDtos register);
    }
}
