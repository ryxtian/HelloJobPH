using HelloJobPH.Server.Service.SuperAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok();
        }
    }
}
