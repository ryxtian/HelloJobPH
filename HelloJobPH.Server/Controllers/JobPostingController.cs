using HelloJobPH.Server.Service.JobPost;
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

        // GET: api/JobPosting
        [HttpGet("list")]
        public async Task<ActionResult<List<JobPostingDtos>>> GetAll()
        
        {
            var jobPosts = await _jobPostService.RetrieveAllAsync();
            return Ok(jobPosts);
        }

        // GET: api/JobPosting/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JobPostingDtos>> GetById(int id)
        {
            var jobPost = await _jobPostService.GetByIdAsync(id);
            if (jobPost == null)
            {
                return NotFound($"Job post with ID {id} not found.");
            }
            return Ok(jobPost);
        }


        [HttpPost]
        public async Task<ActionResult<bool>> Create(JobPostingDtos jobPostDto)
         {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                          var createdJobPost = await _jobPostService.AddAsync(jobPostDto);
            return true;
            //return CreatedAtAction(nameof(GetById), new { id = createdJobPost.JobPostingId }, createdJobPost);
        }

        // PUT: api/JobPosting/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<JobPostingDtos>> Update(int id, JobPostingDtos jobPostDto)
        {
            if (id != jobPostDto.JobPostingId)
            {
                return BadRequest("ID mismatch.");
            }

            var existing = await _jobPostService.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound($"Job post with ID {id} not found.");
            }

            var updatedJobPost = await _jobPostService.UpdateAsync(jobPostDto);
            return Ok(updatedJobPost);
        }

        // DELETE: api/JobPosting/{id}
        [HttpPut("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _jobPostService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound($"Job post with ID {id} not found.");
            }

            return NoContent();
        }
        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            var deleted = await _jobPostService.Activate(id);
            if (!deleted)
            {
                return NotFound($"Job post with ID {id} not found.");
            }

            return NoContent();
        }
        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var deleted = await _jobPostService.Deactivate(id);
            if (!deleted)
            {
                return NotFound($"Job post with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
