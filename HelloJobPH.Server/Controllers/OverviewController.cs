using HelloJobPH.Server.Service.Overview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverviewController : ControllerBase
    {
        private readonly IOverviewService _service;
        public OverviewController(IOverviewService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>ListOverview(int id)
        {
            try
            {
                var result = await _service.ListOverview(id);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
