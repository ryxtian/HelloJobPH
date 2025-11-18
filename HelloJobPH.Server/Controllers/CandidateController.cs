using HelloJobPH.Employer.Pages.JobPost;
using HelloJobPH.Server.GeneralReponse;
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
            var request = await _candidateService.RetrieveAllCandidate();
            return Ok(request);
        }
        [HttpGet("Accepted-List")]
        public async Task<IActionResult> AcceptedList()
        {
            var request = await _candidateService.RetrieveAllAcceptedCandidate();
            return Ok(request);
        }
        [HttpPut("Accept/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> AcceptCandidate(int id)
        {
            var success = await _candidateService.CandidateAccepttAsync(id);
            return Ok(GeneralResponse<bool>.Ok("Candidate accepted successfully.", true));
        }

        [HttpPut("reject/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> RejectCandidate(int id)
        {
            var success = await _candidateService.CandidateRejectAsync(id);
            return Ok(GeneralResponse<bool>.Ok("Candidate rejected successfully.", true));
        }
        [HttpPost("SendEmail")]
        public async Task<ActionResult<GeneralResponse<bool>>> SendEmail([FromBody] SetScheduleDto dto)
        {
            try
            {
                var success = await _candidateService.SendInitialEmail(dto);
                return Ok(GeneralResponse<bool>.Ok("Email sent successfully.", true));
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        [HttpPut("Viewresume/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> ViewResumeUpdate(int id)
        {
            var success = await _candidateService.ViewResumeUpdate(id);
            return Ok(GeneralResponse<bool>.Ok("Resume viewed successfully."+ success));
        }
    }
}
