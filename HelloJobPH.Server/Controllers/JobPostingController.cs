using HelloJobPH.Server.Service.JobPost;
using HelloJobPH.Server.GeneralReponse;
using HelloJobPH.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPostingController : ControllerBase
    {
        private readonly IJobPostService _jobPostService;

        public JobPostingController(IJobPostService jobPostService)
        {
            _jobPostService = jobPostService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<GeneralResponse<List<JobPostingDtos>>>> GetAll()
        {
            var jobPosts = await _jobPostService.RetrieveAllAsync();
            return Ok(jobPosts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<JobPostingDtos>>> GetById(int id)
        {
            var jobPost = await _jobPostService.GetByIdAsync(id);
   
            return Ok(GeneralResponse<JobPostingDtos>.Ok("Job post retrieved successfully."+ jobPost));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<JobPostingDtos>>> Create(JobPostingDtos jobPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralResponse<JobPostingDtos>.Fail("Invalid job post data."));
            }

            var createdJobPost = await _jobPostService.AddAsync(jobPostDto);
            return Ok(GeneralResponse<JobPostingDtos>.Ok("Job post created successfully."));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<JobPostingDtos>>> Update(int id, JobPostingDtos jobPostDto)
        {
            if (id != jobPostDto.JobPostingId)
            {
                return BadRequest(GeneralResponse<JobPostingDtos>.Fail("ID mismatch."));
            }

            var existing = await _jobPostService.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound(GeneralResponse<JobPostingDtos>.Fail($"Job post with ID {id} not found."));
            }

            var updatedJobPost = await _jobPostService.UpdateAsync(jobPostDto);
            return Ok(GeneralResponse<JobPostingDtos>.Ok("Job post updated successfully."));
        }

        [HttpPut("soft-delete/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> SoftDelete(int id)
        {
            var deleted = await _jobPostService.DeleteAsync(id);
            return Ok(GeneralResponse<bool>.Ok("Job post soft-deleted successfully.", true));
        }

        [HttpPut("Activate/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Activate(int id)
        {
            var activated = await _jobPostService.Activate(id);
            return Ok(GeneralResponse<bool>.Ok("Job post activated successfully.", true));
        }

        [HttpPut("Deactivate/{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Deactivate(int id)
        {
            var deactivated = await _jobPostService.Deactivate(id);
            return Ok(GeneralResponse<bool>.Ok("Job post deactivated successfully.", true));
        }
    }
}
