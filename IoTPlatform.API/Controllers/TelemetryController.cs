using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Domain.Models.Telemetries;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TelemetryController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;
        private const string inValidID = "Telemetry ID provided is not a valid ID";

        public TelemetryController(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }

        [HttpPost]
        public async Task<ActionResult> AddTelemetry(Telemetry telemetry)
        {
            var result = await _telemetryService.AddTelemetryAsync(telemetry);
            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTelemetrys()
        {
            var listTelemetry = await _telemetryService.GetAllTelemetrysAsync();
            if (!listTelemetry.Any())
            {
                return NotFound();
            }

            var result = listTelemetry.OrderByDescending(o => o.LastUpdateTime);
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindTelemetryByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

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
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _telemetryService.UpdateTelemetryAsync(id, telemetry);
            if (result == null)
            {
                return NotFound();
            }
                
            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTelemetry(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var obj = await _telemetryService.FindTelemetryByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            var result = await _telemetryService.RemoveTelemetryAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{deviceID}")]
        public async Task<ActionResult> FindTelemetryByDeviceID(string deviceID)
        {
            var isValid = ObjectId.TryParse(deviceID, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var listTelemetry = await _telemetryService.FindTelemetryByDeviceIDAsync(deviceID);
            if (listTelemetry == null)
            {
                return NotFound();
            }

            var result = listTelemetry.OrderByDescending(o => o.LastUpdateTime);
            return new JsonResult(new { result });
        }
    }
}
