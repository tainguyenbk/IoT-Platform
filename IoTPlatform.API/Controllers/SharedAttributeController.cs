using IoTPlatform.Domain.Models.Attributes;
using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SharedAttributeController : ControllerBase
    {
        private readonly ISharedAttributeService _sharedAttributeService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;
        private const string inValidID = "Shared Attribute ID provided is not a valid ID";
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
            var listAttribute = await _sharedAttributeService.GetAllSharedAttributesAsync();
            if (!listAttribute.Any())
            {
                return NotFound();
            }

            var result = listAttribute.OrderByDescending(o => o.LastUpdateTime);
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindSharedAttributeByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

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
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);

            }
            var result = await _sharedAttributeService.UpdateSharedAttributeAsync(id, sharedAttribute);
            if (result == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                "", userInfor[0], userInfor[1], ActionType.UpdateAttribute, status);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveSharedAttribute(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

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
            var isValid = ObjectId.TryParse(deviceID, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var listAttribute = await _sharedAttributeService.FindSharedAttributeByDeviceIDAsync(deviceID);
            if (!listAttribute.Any())
            {
                return NotFound();
            }

            var result = listAttribute.OrderByDescending(o => o.LastUpdateTime);
            return new JsonResult(new { result });
        }
    }
}
