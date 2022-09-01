using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeviceProfileController : ControllerBase
    {
        private readonly IDeviceProfileService _deviceProfileService;

        public DeviceProfileController(IDeviceProfileService deviceProfileService)
        {
            _deviceProfileService = deviceProfileService;
        }

        [HttpPost]
        public async Task<ActionResult> AddDeviceProfile(DeviceProfile deviceProfile)
        {
            var result = await _deviceProfileService.AddDeviceProfileAsync(deviceProfile);
            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDeviceProfiles()
        {
            var result = await _deviceProfileService.GetAllDeviceProfilesAsync();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindDeviceProfileByID(string id)
        {
            var result = await _deviceProfileService.FindDeviceProfileByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDeviceProfile(string id, DeviceProfile deviceProfile)
        {
            var result = await _deviceProfileService.UpdateDeviceProfleAsync(id, deviceProfile);
            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveDeviceProfile(string id)
        {
            var result = await _deviceProfileService.RemoveDeviceProfileAsync(id);
            return new JsonResult(new { result });
        }
    }
}
