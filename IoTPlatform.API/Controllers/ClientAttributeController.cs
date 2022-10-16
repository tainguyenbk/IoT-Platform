using IoTPlatform.Domain.Models.Attributes;
using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientAttributeController : ControllerBase
    {
        private readonly IClientAttributeService _clientAttributeService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;
        private const string inValidID = "Audit Log ID provided is not a valid ID";

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
            var listAttribute = await _clientAttributeService.GetAllClientAttributesAsync();
            if (!listAttribute.Any())
            {
                return NotFound();
            }

            var result = listAttribute.OrderByDescending(o => o.LastUpdateTime);
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindClientAttributeByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

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
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }
            var result = await _clientAttributeService.UpdateClientAttributeAsync(id, clientAttribute);
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
        public async Task<ActionResult> RemoveClientAttribute(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }
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
            var isValid = ObjectId.TryParse(deviceID, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }
            var listAttribute = await _clientAttributeService.FindClientAttributeByDeviceIDAsync(deviceID);
            if(!listAttribute.Any())
            {
                return NotFound();
            }

            var result = listAttribute.OrderByDescending(o => o.LastUpdateTime);
            return new JsonResult(new { result });
        }
    }
}
