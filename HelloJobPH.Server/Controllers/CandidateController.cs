using HelloJobPH.Employer.Pages.JobPost;
using HelloJobPH.Server.Service.Candidate;
using HelloJobPH.Shared.DTOs;
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
        [HttpGet("Accepted-List")]
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
        [HttpPut("Accept/{id}")]
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

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectCandidate(int id)
        {
            try
            {
                var request = await _candidateService.CandidateRejectAsync(id);
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
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] SetScheduleDto dto)
        {
            try
            {
                var request = await _candidateService.SendInitialEmail(dto);
                if (!request)
                {
                    return NotFound($"Application with ID {dto.ApplicationId} not found.");
                }
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("Viewresume/{id}")]
        public async Task<IActionResult>ViewResumeUpdate(int id)
        {
            var result = await _candidateService.ViewResumeUpdate(id);
            return Ok(result);
        }

    }
}
