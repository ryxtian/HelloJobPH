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
            try
            {
                var request = await _service.InitialList();
                return Ok(request);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("technical")]
        public async Task<IActionResult> TechnicalList()
        {
            try
            {
                var request = await _service.TechnicalList();
                return Ok(request);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("final")]
        public async Task<IActionResult> FinalList()
        {
            try
            {
                var request = await _service.FinalList();
                return Ok(request);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("Reschedule")]
        public async Task<IActionResult> Reschedule(SetScheduleDto dto)
        {
            try
            {
                var request = await _service.Reschedule(dto);
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
        [HttpGet("NoAppearance{id}")]
        public async Task<IActionResult>NoAppearance(int id)
        {
            try
            {
                var request = await _service.NoAppearance(id);
                if (request == 0)
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
        [HttpPost("ForTechnical")]
        public async Task<IActionResult> ForTechnical(SetScheduleDto dto)
        {
            try
            {
                var request = await _service.ForTechnical(dto);
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
        [HttpPost("ForFinal")]
        public async Task<IActionResult> ForFinal(SetScheduleDto dto)
        {
            try
            {
                var request = await _service.ForFinal(dto);
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
        [HttpGet("Failed{id}")]
        public async Task<IActionResult> Failed(int id)
        {
            try
            {
                var request = await _service.Failed(id);
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
        [HttpGet("Delete{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var request = await _service.DeleteApplication(id);
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
        [HttpGet("MarkAsCompleted{id}")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            try
            {
                var request = await _service.MarkAsCompleted(id);
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
