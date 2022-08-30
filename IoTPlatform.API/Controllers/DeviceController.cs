using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPost]
        public async Task<ActionResult> AddDevice(Device device)
        {
            var result = await _deviceService.AddDeviceAsync(device);
            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDevices()
        {
            var result = await _deviceService.GetAllDevicesAsync();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindDeviceByID(string id)
        {
            var result = await _deviceService.FindDeviceByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDevice(string id, Device device)
        {
            var result = await _deviceService.UpdateDeviceAsync(id, device);
            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveDevice(string id)
        {
            var result = await _deviceService.RemoveDeviceAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> FindDeviceByName(string name)
        {
            var result = await _deviceService.FindDeviceByNameAsync(name);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{deviceProfile}")]
        public async Task<ActionResult> FindDeviceByDeviceProfile(string deviceProfile)
        {
            var result = await _deviceService.FindDeviceByDeviceProfileAsync(deviceProfile);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{label}")]
        public async Task<ActionResult> FindDeviceByLabel(string label)
        {
            var result = await _deviceService.FindDeviceByLabelAsync(label);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{customer}")]
        public async Task<ActionResult> FindDeviceByCustomer(string customer)
        {
            var result = await _deviceService.FindDeviceByIdAsync(customer);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }
    }
}
