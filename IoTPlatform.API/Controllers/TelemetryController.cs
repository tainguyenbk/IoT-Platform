using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TelemetryController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;

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
            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTelemetry(string id)
        {
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
