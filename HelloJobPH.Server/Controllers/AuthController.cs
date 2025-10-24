
using HelloJobPH.Server.Service.ApplicantRepo;
using HelloJobPH.Server.Service.Auth;
using HelloJobPH.Server.Service.UserAccountRepository;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.Authorization;
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

        public AuthController(IAuthService authService,IConfiguration configuration, IUserAccountRepository userRepo, IApplicantRepository applicantRepo)
        {
            _authService = authService;

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDtos user)
        {
            try
            {
                var token = await _authService.LoginAsync(user.Email, user.Password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("claims")]
        [Authorize] // Require valid token
        public IActionResult GetClaims()
        {
            // Ensure user is authenticated
            if (User.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
                return Unauthorized();

            // Extract claims from token
            var claims = identity.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });

            return Ok(claims);
        }
    }
}
