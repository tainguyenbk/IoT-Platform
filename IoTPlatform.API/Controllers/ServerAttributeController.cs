using IoTPlatform.Domain.Models.Attributes;
using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServerAttributeController : ControllerBase
    {
        private readonly IServerAttributeService _serverAttributeService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;
        private const string inValidID = "Server Attribute ID provided is not a valid ID";
        public ServerAttributeController(IServerAttributeService serverAttributeService, IUserService userService, IAuditLogService auditLogService)
        {
            _serverAttributeService = serverAttributeService;
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
        public async Task<ActionResult> AddServerAttribute(ServerAttribute serverAttribute)
        {
            var result = await _serverAttributeService.AddServerAttributeAsync(serverAttribute);

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                "", userInfor[0], userInfor[1], ActionType.CreateAttribute, status);

            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllServerAttributes()
        {
            var listAttribute = await _serverAttributeService.GetAllServerAttributesAsync();
            if (listAttribute.Count() == 0)
            {
                return NotFound();
            }

            var result = listAttribute.OrderByDescending(o => o.LastUpdateTime);
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindServerAttributeByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _serverAttributeService.FindServerAttributeByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateServerAttribute(string id, ServerAttribute serverAttribute)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _serverAttributeService.UpdateServerAttributeAsync(id, serverAttribute);
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
        public async Task<ActionResult> RemoveServerAttribute(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var obj = await _serverAttributeService.FindServerAttributeByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            var result = await _serverAttributeService.RemoveServerAttributeAsync(id);

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, obj.DeviceID,
                "", userInfor[0], userInfor[1], ActionType.DeleteAttribute, status);
            return new JsonResult(new { result });
        }

        [HttpGet("{deviceID}")]
        public async Task<ActionResult> FindServerAttributeByDeviceID(string deviceID)
        {
            var isValid = ObjectId.TryParse(deviceID, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var listAttribute = await _serverAttributeService.FindServerAttributeByDeviceIDAsync(deviceID);
            if (!listAttribute.Any())
            {
                return NotFound();
            }

            var result = listAttribute.OrderByDescending(o => o.LastUpdateTime);
            return new JsonResult(new { result });
        }
    }
}
