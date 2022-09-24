using IoTPlatform.Domain.Models.AuditLog;
using IoTPlatform.Domain.Models.Telemetry;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TelemetryController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;

        public TelemetryController(ITelemetryService telemetryService, IUserService userService, IAuditLogService auditLogService)
        {
            _telemetryService = telemetryService;
            _userService = userService;
            _auditLogService = auditLogService;
        }

        [HttpPost]
        public async Task<ActionResult> AddTelemetry(Telemetry telemetry)
        {
            var result = await _telemetryService.AddTelemetryAsync(telemetry);

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Telemetry, result.TelemetryID,
                "", userInfor[0], userInfor[1], ActionType.Create);

            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTelemetrys()
        {
            var result = await _telemetryService.GetAllTelemetrysAsync();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindTelemetryByID(string id)
        {
            var result = await _telemetryService.FindTelemetryByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTelemetry(string id, Telemetry telemetry)
        {
            var result = await _telemetryService.UpdateTelemetryAsync(id, telemetry);

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Telemetry, result.TelemetryID,
                "", userInfor[0], userInfor[1], ActionType.Update);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTelemetry(string id)
        { 
            var removeTelemetry = await _telemetryService.FindTelemetryByIdAsync(id);
            if (removeTelemetry == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Telemetry, removeTelemetry.TelemetryID,
                "", userInfor[0], userInfor[1], ActionType.Delete);

            var result = await _telemetryService.RemoveTelemetryAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{deviceID}")]
        public async Task<ActionResult> FindTelemetryByDeviceID(string deviceID)
        {
            var result = await _telemetryService.FindTelemetryByDeviceIDAsync(deviceID);
            return new JsonResult(new { result });
        }
    }
}
