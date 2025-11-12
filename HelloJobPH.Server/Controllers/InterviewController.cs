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
        public async Task<IActionResult> InitialList()
        {
            var request = await _service.InitialList();
            return Ok(request);
        }
        [HttpGet("technical")]
        public async Task<IActionResult> TechnicalList()
        {
            var request = await _service.TechnicalList();
            return Ok(request);
        }
        [HttpGet("final")]
        public async Task<IActionResult> FinalList()
        {
            var request = await _service.FinalList();
            return Ok(request);
        }
        [HttpPost("Reschedule")]
        public async Task<ActionResult<GeneralResponse<bool>>> Reschedule(SetScheduleDto dto)
        {
            var request = await _service.Reschedule(dto);
            return Ok(GeneralResponse<bool>.Ok("Application rescheduled successfully.", true));
        }
        [HttpGet("NoAppearance{id}")]
        public async Task<ActionResult<GeneralResponse<int>>> NoAppearance(int id)
        {
            var result = await _service.NoAppearance(id);
            return Ok(GeneralResponse<int>.Ok("No appearance count retrieved successfully."+ result));
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
        [HttpGet("Delete/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Delete(int id)
        {
            var request = await _service.DeleteApplication(id);
            return Ok(GeneralResponse<bool>.Ok("Application deleted successfully.", true));
        }
        [HttpGet("MarkAsCompleted/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> MarkAsCompleted(int id)
        {
            var request = await _service.MarkAsCompleted(id);
            return Ok(GeneralResponse<bool>.Ok("Application marked as completed.", true));
        }
    }
}
