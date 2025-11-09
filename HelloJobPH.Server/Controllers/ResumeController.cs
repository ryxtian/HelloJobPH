using HelloJobPH.Server.Service.Resume;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [HttpGet("get-resume/{applicationId}")]
        public async Task<IActionResult> GetResume(int applicationId)
        {
            var fileBytes = await _resumeService.GetResumeBytesAsync(applicationId);
            if (fileBytes == null || fileBytes.Length == 0)
                return NotFound();

            // Return as PDF
            return File(fileBytes, "application/pdf", $"Resume_{applicationId}.pdf");
        }
    }

}
