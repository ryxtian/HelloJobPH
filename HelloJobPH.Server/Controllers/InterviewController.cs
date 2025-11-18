using HelloJobPH.Server.GeneralReponse;
using HelloJobPH.Server.Service.Interview;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        private readonly IInterviewService _service;
        public InterviewController(IInterviewService service)
        {
            _service = service;
        }
        [HttpGet("initial")]
        public async Task<ActionResult<GeneralResponse<List<InterviewListDtos>>>> InitialList()
        {
            try
            {
                var request = await _service.InitialList();
                return Ok(request);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        //[HttpGet("technical")]
        //public async Task<IActionResult> TechnicalList()
        //{
        //    var request = await _service.TechnicalList();
        //    return Ok(request);
        //}
        //[HttpGet("final")]
        //public async Task<IActionResult> FinalList()
        //{
        //    var request = await _service.FinalList();
        //    return Ok(request);
        //}
        [HttpPost("Reschedule")]
        public async Task<ActionResult<GeneralResponse<bool>>> Reschedule(SetScheduleDto dto)
        {
            var request = await _service.Reschedule(dto);
            return Ok(GeneralResponse<bool>.Ok("Application rescheduled successfully.", true));
        }
        [HttpPut("NoAppearance/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> NoAppearance(int id)
        {
            var result = await _service.NoAppearance(id);
            // Assuming the service returns a boolean indicating success
            return GeneralResponse<bool>.Ok("No appearance processed successfully."+ result);
        }

        [HttpPost("ForTechnical")]
        public async Task<ActionResult<GeneralResponse<bool>>> ForTechnical(SetScheduleDto dto)
        {
            var request = await _service.ForTechnical(dto);
            return Ok(GeneralResponse<bool>.Ok("Marked for technical successfully.", true));
        }
        [HttpPost("ForFinal")]
        public async Task<ActionResult<GeneralResponse<bool>>> ForFinal(SetScheduleDto dto)
        {
            var request = await _service.ForFinal(dto);
            return Ok(GeneralResponse<bool>.Ok("Marked for final successfully.", true));
        }
        [HttpGet("Failed/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Failed(int id)
        {
            var request = await _service.Failed(id);
            return Ok(GeneralResponse<bool>.Ok("Application marked as failed.", true));
        }
        [HttpPut("Delete/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Delete(int id)
        {
            var request = await _service.DeleteApplication(id);
            return Ok(GeneralResponse<bool>.Ok("Application deleted successfully.", true));
        }
        [HttpPut("MarkAsCompleted/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> MarkAsCompleted(int id)
        {
            var request = await _service.MarkAsCompleted(id);
            return Ok(GeneralResponse<bool>.Ok("Application marked as completed.", true));
        }
        [HttpPut("hired/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Hired(int id)
        {
            var request = await _service.Hired(id);
            return Ok(GeneralResponse<bool>.Ok("Hire.", true));
        }
            [HttpGet("interviewer-List")]
        public async Task<IActionResult> Interviewerlist()
        {
            var request = await _service.InterviewerList();
            return Ok(request);
        }
    }
}
