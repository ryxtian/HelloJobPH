using HelloJobPH.Server.Service.SuperAdmin;
using HelloJobPH.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HelloJobPH.Employer.Pages.SuperAdmin.AdminDashboard;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly ISuperAdminService _service;
        public SuperAdminController(ISuperAdminService service)
        {
            _service = service;
        }
        [HttpGet("employer-list")]
        public async Task<IActionResult> EmployerList()
        {
            var lsit = await _service.EmployersList();
            return Ok(lsit);
        }
        [HttpGet("ForApprovalList")]
        public async Task<IActionResult> ForApprovalList()
        {
            var lsit = await _service.ForApprovalList();
            return Ok(lsit);
        }
        [HttpPut("approve-employer/{id}")]
        public async Task<IActionResult> ApproveEmployer(int id)
        {
            var result = await _service.ApprovedEmployer(id);
            return result ? Ok() : BadRequest();
        }

        [HttpPut("disable-employer/{id}")]
        public async Task<IActionResult> DisableEmployer(int id)
        {
            var result = await _service.DisableEmployer(id);
            return result ? Ok() : BadRequest();
        }

        [HttpPut("disapprove-employer/{id}")]
        public async Task<IActionResult> DisapproveEmployer(int id)
        {
            var result = await _service.DisapprovedEmployer(id);
            return result ? Ok() : BadRequest();
        }
        [HttpGet("TotalJobs")]
        public async Task<ActionResult<int>> TotalJobs()
        {
            var totalJobs = await _service.TotalJobCounts();
            return Ok(totalJobs);
        }

        [HttpGet("TotalApplicants")]
        public async Task<ActionResult<int>> TotalApplicants()
        {
            var totalApplicants = await _service.TotalApplicants();
            return Ok(totalApplicants);
        }
        [HttpGet("ScheduledInterviews")]
        public async Task<ActionResult<int>> ScheduledInterviews()
        {
            var upcomingInterviews = await _service.ScheduledInterviews();

            return Ok(upcomingInterviews);
        }
        [HttpGet("SuccessfulHires")]
        public async Task<ActionResult<int>> SuccessfulHires()
        {
            var successfulHires = await _service.SuccessfulHires();
   
            return Ok(successfulHires);
        }
        [HttpGet("TopAppliedJobs")]
        public async Task<ActionResult<List<TopJobDto>>> TopAppliedJobs() => Ok(await _service.GetTopAppliedJobs());
        [HttpGet("applicationstatus")]
        public async Task<List<ApplicationStatusDto>> ApplicationStatus() =>
         await _service.GetApplicationStatusCounts();

        [HttpGet("JobPostList")]
        public async Task<IActionResult> JobPostsList()
        {
            var lsit = await _service.JobPostList();
            return Ok(lsit);
        }
        [HttpGet("ApplicantList")]
        public async Task<IActionResult> ApplicantLists()
        {
            var lsit = await _service.ApplicantList();
            return Ok(lsit);
        }

        [HttpPut("block")]
        public async Task<IActionResult> BlockApplicant([FromQuery] int id)
        {
            var result = await _service.BlockApplicantAsync(id);
            if (!result)
                return NotFound(new { message = "Applicant not found or already blocked." });

            return Ok(true);
        }

        [HttpPut("unblock")]
        public async Task<IActionResult> UnBlockApplicant([FromQuery] int id)
        {
            var result = await _service.UnBlockApplicantAsync(id);
            if (!result)
                return NotFound(new { message = "Applicant not found or already unblocked." });

            return Ok(true);
        }

  
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicantDtos>> ViewApplicant(int id)
        {
            var applicant = await _service.ViewApplicantAsync(id);
            if (applicant == null)
                return NotFound(new { message = "Applicant not found." });

            return Ok(applicant);
        }
    }
}
