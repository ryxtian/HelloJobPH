using HelloJobPH.Server.Service.AI;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static HelloJobPH.Employer.Services.Candidate.ClientCandidateService;

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

        [HttpPost("ai-overview/{id}")]
        public async Task<IActionResult> Generate(int id)
        {
            var result = await _aiService.GenerateOverviewAsync(id);

            return Ok(new OverviewResponse { Overview = result });
        }
    }

    public class OverviewRequest
    {
        public string ResumeText { get; set; }
        public string JobPostText { get; set; }
    }
}