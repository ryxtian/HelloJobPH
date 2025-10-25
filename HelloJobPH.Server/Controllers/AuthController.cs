using HelloJobPH.Employer.Services.Authentication;
using HelloJobPH.Server.Service.Auth;
using HelloJobPH.Server.Services;
using HelloJobPH.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDtos user)
        {
            try
            {
                var token = await _authService.LoginAsync(user.Email, user.Password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
