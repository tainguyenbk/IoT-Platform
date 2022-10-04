using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Domain.Models.DeviceProfiles;
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
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;

        public DeviceProfileController(IDeviceProfileService deviceProfileService, IWebHostEnvironment webHostEnvironment, IUserService userService, IAuditLogService auditLogService)
        {
            _deviceProfileService = deviceProfileService;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _auditLogService = auditLogService;
        }

        [HttpPost]
        public async Task<ActionResult> AddDeviceProfile(DeviceProfile deviceProfile)
        {
            var result = await _deviceProfileService.AddDeviceProfileAsync(deviceProfile);

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.DeviceProfile, result.DeviceProfileID, 
                result.DeviceProfileName, userInfor[0], userInfor[1], ActionType.Create);
            
            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDeviceProfiles()
        {
            var result = await _deviceProfileService.GetAllDeviceProfilesAsync();
            if (!result.Any())
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindDeviceProfileByID(string id)
        {
            var result = await _deviceProfileService.FindDeviceProfileDetailByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDeviceProfile(string id, DeviceProfile deviceProfile)
        {
            var obj = await _deviceProfileService.FindDeviceProfileByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            var result = await _deviceProfileService.UpdateDeviceProfleAsync(id, deviceProfile);

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.DeviceProfile, result.DeviceProfileID, 
                result.DeviceProfileName, userInfor[0], userInfor[1], ActionType.Update);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveDeviceProfile(string id)
        {
            var obj = await _deviceProfileService.FindDeviceProfileByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.DeviceProfile, obj.DeviceProfileID,
                obj.DeviceProfileName, userInfor[0], userInfor[1], ActionType.Delete);

            var result = await _deviceProfileService.RemoveDeviceProfileAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> FindDeviceProfileByName(string name)
        {
            var result = await _deviceProfileService.FindDeviceProfileByNameAsync(name);
            if (!result.Any())
            {
                return NotFound();
            }    
            return new JsonResult(new { result });
        }

        [HttpGet("{ruleChainID}")]
        public async Task<ActionResult> FindDeviceProfileByRuleChainID(string ruleChainID)
        {
            var result = await _deviceProfileService.FindDeviceProifleByRuleChainIDAsync(ruleChainID);
            if (!result.Any())
            {
                return NotFound();
            }
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

        [HttpPost("{id}")]
        public async Task<ActionResult> MakeDeviceProfileDefault(string id)
        {
            var obj = await _deviceProfileService.FindDeviceProfileByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            var result = await _deviceProfileService.MakeDeviceProfileDefaultAsync(id);
            return new JsonResult(new { result });
        }
    }
}
