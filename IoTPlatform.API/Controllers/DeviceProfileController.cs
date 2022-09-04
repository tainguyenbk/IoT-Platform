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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeviceProfileController(IDeviceProfileService deviceProfileService, IWebHostEnvironment webHostEnvironment)
        {
            _deviceProfileService = deviceProfileService;
            _webHostEnvironment = webHostEnvironment;

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

        [HttpGet("{name}")]
        public async Task<ActionResult> FindDeviceProfileByName(string name)
        {
            var result = await _deviceProfileService.FindDeviceProfileByNameAsync(name);
            return new JsonResult(new { result });
        }

        [HttpPost]
        public async Task<ActionResult> UploadDeviceProfileImage(string id, List<IFormFile> files)
        {
            List<DeviceProfileImage> images = new List<DeviceProfileImage>();
            string fileFolder = @"Files\Images\";

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    try
                    {
                        if (!Directory.Exists(fileFolder))
                        {
                            Directory.CreateDirectory(fileFolder);
                        }

                        using (FileStream fileStream = System.IO.File.Create(fileFolder + formFile.FileName))
                        {
                            await formFile.CopyToAsync(fileStream);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new JsonResult(new { V = ex.ToString()});
                    }

                    var filePath = fileFolder + formFile.FileName;
                    var image = new DeviceProfileImage()
                    {
                        FilePath = filePath
                    };
                    images.Add(image);
                }
            }
            var result = await _deviceProfileService.UploadImageAsync(id, images);
            return new JsonResult(new { result });
        }
    }
}
