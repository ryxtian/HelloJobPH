using HelloJobPH.Server.Service.Candidate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var request = await _candidateService.RetrieveAllCandidate();
                return Ok(request);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        
        }
    }
}
