using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAuditLogs()
        {
            var result = await _auditLogService.GetAllAuditLogs();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> FindAuditLogByID(string id)
        {
            var result = await _auditLogService.FindAuditLogByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{entityType}")]
        public async Task<ActionResult> FindAuditLogByEntityTypeName(string entityType)
        {
            var result = await _auditLogService.FindAuditLogByEntityTypeNameAsync(entityType);
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }


        [HttpGet("{userName}")]
        public async Task<ActionResult> FindAuditLogByUserName(string userName)
        {
            var result = await _auditLogService.FindAuditLogByUserNameAsync(userName);
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }


        [HttpGet("{startTime}&&{endTime}")]
        public async Task<ActionResult> FindAuditLogInPeriodTime(DateTime startTime, DateTime endTime)
        {
            var result = await _auditLogService.FindAuditLogInPeriodTimeAsync(startTime, endTime);
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> FindAuditLogs(string? auditLogID, string? entityTypeName, string? userName, DateTime? startTime, DateTime? endTime)
        {
            var result = await _auditLogService.FindAuditLogsAsync(auditLogID, entityTypeName, userName, startTime, endTime);
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }
    }
}
