using IoTPlatform.Domain.Models.Attributes;
using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SharedAttributeController : ControllerBase
    {
        private readonly ISharedAttributeService _sharedAttributeService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;

        public SharedAttributeController(ISharedAttributeService sharedAttributeService, IUserService userService, IAuditLogService auditLogService)
        {
            _sharedAttributeService = sharedAttributeService;
            _userService = userService;
            _auditLogService = auditLogService;
        }

        private string GetResponseStatus()
        {
            string status;
            if (Response.StatusCode == 200)
            {
                status = "Success";
            }
            else
            {
                status = "Failure";
            }
            return status;
        }

        [HttpPost]
        public async Task<ActionResult> AddSharedAttribute(SharedAttribute sharedAttribute)
        {
            var result = await _sharedAttributeService.AddSharedAttributeAsync(sharedAttribute);

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                "", userInfor[0], userInfor[1], ActionType.CreateAttribute, status);

            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSharedAttributes()
        {
            var result = await _sharedAttributeService.GetAllSharedAttributesAsync();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindSharedAttributeByID(string id)
        {
            var result = await _sharedAttributeService.FindSharedAttributeByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSharedAttribute(string id, SharedAttribute sharedAttribute)
        {
            var result = await _sharedAttributeService.UpdateSharedAttributeAsync(id, sharedAttribute);

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                "", userInfor[0], userInfor[1], ActionType.UpdateAttribute, status);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveSharedAttribute(string id)
        {             
            var obj = await _sharedAttributeService.FindSharedAttributeByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, obj.DeviceID,
                "", userInfor[0], userInfor[1], ActionType.DeleteAttribute, status);

            var result = await _sharedAttributeService.RemoveSharedAttributeAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{deviceID}")]
        public async Task<ActionResult> FindSharedAttributeByDeviceID(string deviceID)
        {
            var result = await _sharedAttributeService.FindSharedAttributeByDeviceIDAsync(deviceID);
            return new JsonResult(new { result });
        }
    }
}
