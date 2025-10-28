using HelloJobPH.Server.Service.Interview;
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
        public async Task<IActionResult>InitialList()
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
        [HttpGet("Reschedule{id}")]
        public async Task<IActionResult> Reschedule(int id, string date, string time, string? location)
        {
            try
            {
                var request = await _service.Reschedule(id, date, time, location);
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
        [HttpGet("ForTechnical{id}")]
        public async Task<IActionResult> ForTechnical(int id, string date, string time, string? location)
        {
            try
            {
                var request = await _service.ForTechnical(id, date, time, location);
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
        [HttpGet("ForFinal{id}")]
        public async Task<IActionResult> ForFinal(int id, string date, string time, string? location)
        {
            try
            {
                var request = await _service.ForFinal(id, date, time, location);
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
    }
}
