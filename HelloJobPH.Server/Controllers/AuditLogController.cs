using HelloJobPH.Server.Service.AuditLog;
using HelloJobPH.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditService;
        public AuditLogController(IAuditLogService auditService)
        {
            _auditService = auditService;
        }
        [HttpGet("audit-list")]
        public async Task<ActionResult<List<AuditLogDtos>>> GetAuditLogs()
        {
            var logs = await _auditService.GetAuditLogsAsync();
            return Ok(logs);
        }
    }
}
