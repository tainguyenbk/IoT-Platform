using IoTPlatform.Domain.Models;
using IoTPlatform.Domain.Models.AuditLog;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientAttributeController : ControllerBase
    {
        private readonly IClientAttributeService _clientAttributeService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;

        public ClientAttributeController(IClientAttributeService clientAttributeService, IUserService userService, IAuditLogService auditLogService)
        {
            _clientAttributeService = clientAttributeService;
            _userService = userService;
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllClientAttributes()
        {
            var result = await _clientAttributeService.GetAllClientAttributesAsync();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindClientAttributeByID(string id)
        {
            var result = await _clientAttributeService.FindClientAttributeByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClientAttribute(string id, ClientAttribute clientAttribute)
        {
            var result = await _clientAttributeService.UpdateClientAttributeAsync(id, clientAttribute);

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.ClientAttribute, result.AttributeID, "", userInfor[0], userInfor[1], ActionType.Create);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveClientAttribute(string id)
        {
            var removeClientAttribute = await _clientAttributeService.FindClientAttributeByIdAsync(id);
            if (removeClientAttribute == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.ClientAttribute, removeClientAttribute.AttributeID, "", userInfor[0], userInfor[1], ActionType.Delete);
            
            var result = await _clientAttributeService.RemoveClientAttributeAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{deviceID}")]
        public async Task<ActionResult> FindClientAttributeByDeviceID(string deviceID)
        {
            var result = await _clientAttributeService.FindClientAttributeByDeviceIDAsync(deviceID);
            return new JsonResult(new { result });
        }
    }
}
