using HelloJobPH.Server.Service.Dashboard;
using HelloJobPH.Server.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("TotalJobPost")]
        public async Task<IActionResult> TotalJobPost()
        {
            try
            {
                var result = await _dashboardService.GetTotalJobPostAsync();
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest();
            }
  
        }

        [HttpGet("ActiveApplication")]
        public async Task<IActionResult> ActiveApplication()
        {
            try
            {
                var result = await _dashboardService.GetActiveApplicationAsync();
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpGet("ApprovedHired")]
        public async Task<IActionResult> ApprovedHired()
        {
            try
            {
                var result = await _dashboardService.GetApprovedHiredAsync();
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpGet("GetMonthlyApplicantsAsync")]
        public async Task<IActionResult> GetMonthlyApplicantsAsync()
        {
            var result = await _dashboardService.GetMonthlyApplicantsAsync();
            return Ok(result);
        }
    }
}
