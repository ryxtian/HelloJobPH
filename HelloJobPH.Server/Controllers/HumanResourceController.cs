
using HelloJobPH.Server.Service.HumanResource;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanResourceController : ControllerBase
    {
        private readonly IHumanResourceService _humanService;
        public HumanResourceController(IHumanResourceService humanService)
        {
            _humanService = humanService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _humanService.RetrieveAllAsync();
                return Ok(list);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _humanService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound($"Job post with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
