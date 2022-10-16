using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
            var listAuditLog = await _auditLogService.GetAllAuditLogs();
            if (!listAuditLog.Any())
            {
                return NotFound();
            }

            var result = listAuditLog.OrderByDescending(o => o.TimeStamp);
            return new JsonResult(new { result });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> FindAuditLogByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest("Audit Log ID provided is not a valid ID");
            }

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
            var listAuditLog = await _auditLogService.FindAuditLogByEntityTypeNameAsync(entityType);
            if (!listAuditLog.Any())
            {
                return NotFound();
            }

            var result = listAuditLog.OrderByDescending(o => o.TimeStamp);
            return new JsonResult(new { result });
        }


        [HttpGet("{userName}")]
        public async Task<ActionResult> FindAuditLogByUserName(string userName)
        {
            var listAuditLog = await _auditLogService.FindAuditLogByUserNameAsync(userName);
            if (!listAuditLog.Any())
            {
                return NotFound();
            }

            var result = listAuditLog.OrderByDescending(o => o.TimeStamp);
            return new JsonResult(new { result });
        }


        [HttpGet("{startTime}&&{endTime}")]
        public async Task<ActionResult> FindAuditLogInPeriodTime(DateTime startTime, DateTime endTime)
        {
            var listAuditLog = await _auditLogService.FindAuditLogInPeriodTimeAsync(startTime, endTime);
            if (!listAuditLog.Any())
            {
                return NotFound();
            }

            var result = listAuditLog.OrderByDescending(o => o.TimeStamp);
            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> FindAuditLogs(string? auditLogID, string? entityTypeName, string? userName, DateTime? startTime, DateTime? endTime)
        {
            var listAuditLog = await _auditLogService.FindAuditLogsAsync(auditLogID, entityTypeName, userName, startTime, endTime);
            if (!listAuditLog.Any())
            {
                return NotFound();
            }

            var result = listAuditLog.OrderByDescending(o => o.TimeStamp);
            return new JsonResult(new { result });
        }
    }
}
