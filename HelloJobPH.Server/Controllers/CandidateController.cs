using HelloJobPH.Employer.Pages.JobPost;
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
        [HttpGet("AcceptedList")]
        public async Task<IActionResult> AcceptedList()
        {
            try
            {
                var request = await _candidateService.RetrieveAllAcceptedCandidate();
                return Ok(request);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> AcceptCandidate(int id)
        {
            try
            {
                var request = await _candidateService.CandidateAccepttAsync(id);
                if (request == false)
                {
                    return NotFound($"Application with ID {id} not found.");
                }
                return Ok(request);
            }
            catch (Exception)
            {

                throw;
            }
         
        }
        [HttpGet("SendEmail{id}")]
        public async Task<IActionResult> SendEmail(int id, string date, string time, string? location)
        {
            try
            {
                var request = await _candidateService.SendInitialEmail(id, date, time, location);
                if (!request)
                {
                    return NotFound($"Application with ID {id} not found.");
                }
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
