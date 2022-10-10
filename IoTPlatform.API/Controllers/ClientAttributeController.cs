using IoTPlatform.Domain.Models.Attributes;
using IoTPlatform.Domain.Models.AuditLogs;
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
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                "", userInfor[0], userInfor[1], ActionType.UpdateAttribute, status);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveClientAttribute(string id)
        {
            var obj = await _clientAttributeService.FindClientAttributeByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, obj.DeviceID,
                "", userInfor[0], userInfor[1], ActionType.DeleteAttribute, status);

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
