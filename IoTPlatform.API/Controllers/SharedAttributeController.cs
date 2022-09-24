using IoTPlatform.Domain.Models;
using IoTPlatform.Domain.Models.AuditLog;
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

        [HttpPost]
        public async Task<ActionResult> AddSharedAttribute(SharedAttribute sharedAttribute)
        {
            var result = await _sharedAttributeService.AddSharedAttributeAsync(sharedAttribute);

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.SharedAttribute, result.AttributeID,
                "", userInfor[0], userInfor[1], ActionType.Create);

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
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.SharedAttribute, result.AttributeID,
                "", userInfor[0], userInfor[1], ActionType.Update);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveSharedAttribute(string id)
        {             
            var removeAttribute = await _sharedAttributeService.FindSharedAttributeByIdAsync(id);
            if (removeAttribute == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.SharedAttribute, removeAttribute.AttributeID,
                "", userInfor[0], userInfor[1], ActionType.Delete);

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
