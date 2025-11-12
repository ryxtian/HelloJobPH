using HelloJobPH.Server.GeneralReponse;
using HelloJobPH.Server.Service.Overview;
using HelloJobPH.Shared.DTOs;
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
        public async Task<ActionResult<GeneralResponse<OverviewDtos>>>ListOverview(int id)
        {
            var result = await _service.ListOverview(id);
            return GeneralResponse<OverviewDtos>.Ok("Overview success");
        }
    }
}
