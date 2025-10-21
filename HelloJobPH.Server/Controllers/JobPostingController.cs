using HelloJobPH.Server.Service.JobPost;
using HelloJobPH.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        private readonly IJobPostRepository _jobRepo;
        public JobPostingController(IJobPostRepository jobRepo)
        {
            _jobRepo = jobRepo;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var request = await _jobRepo.RetrieveAllAsync();
                return Ok(request);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult>AddPost(JobPostingDtos post)
        {
            try
            {
                var createdJobPost = await _jobRepo.AddAsync(post);
                return Ok(new { message = "Job post created successfully", jobPostId = createdJobPost.JobPostId });
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
