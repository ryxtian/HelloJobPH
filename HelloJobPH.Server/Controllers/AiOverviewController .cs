using Microsoft.AspNetCore.Mvc;
using HelloJobPH.Server.Service.AI;
using System.Threading.Tasks;

namespace HelloJobPH.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiOverviewController : ControllerBase
    {
        private readonly AiOverviewService _aiService;

        public AiOverviewController(AiOverviewService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] OverviewRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.ResumeText) || string.IsNullOrWhiteSpace(request?.JobPostText))
                return BadRequest("Both ResumeText and JobPostText are required.");

            var result = await _aiService.GenerateOverviewAsync(request.ResumeText, request.JobPostText);
            return Ok(new { overview = result });
        }

    }

    public class OverviewRequest
    {
        public string ResumeText { get; set; }
        public string JobPostText { get; set; }
    }
}