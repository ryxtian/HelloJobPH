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
    }
}
