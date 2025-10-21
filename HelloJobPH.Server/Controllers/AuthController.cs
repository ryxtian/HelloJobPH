
using HelloJobPH.Server.Service.ApplicantRepo;
using HelloJobPH.Server.Service.Auth;
using HelloJobPH.Server.Service.UserAccountRepository;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IUserAccountRepository _userRepo;
        private readonly IApplicantRepository _applicantRepo;
        public AuthController(IAuthService authService,IConfiguration configuration, IUserAccountRepository userRepo, IApplicantRepository applicantRepo)
        {
            _authService = authService;
            _configuration = configuration;
            _userRepo = userRepo;
            _applicantRepo = applicantRepo;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email,string passsword)
        {
            try
            {
                var token = await _authService.LoginAsync(email, passsword);
                return Ok(new { token });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult>Register(RegisterDtos user)
        {
            try
            {
                var request = await _authService.RegisterAsync(user);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
