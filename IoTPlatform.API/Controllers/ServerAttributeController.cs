using IoTPlatform.Domain.Models.Attributes;
using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServerAttributeController : ControllerBase
    {
        private readonly IServerAttributeService _serverAttributeService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;
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
            var result = await _serverAttributeService.GetAllServerAttributesAsync();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindServerAttributeByID(string id)
        {
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
            var result = await _serverAttributeService.UpdateServerAttributeAsync(id, serverAttribute);

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                "", userInfor[0], userInfor[1], ActionType.UpdateAttribute, status);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveServerAttribute(string id)
        {   
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
            var result = await _serverAttributeService.FindServerAttributeByDeviceIDAsync(deviceID);
            return new JsonResult(new { result });
        }
    }
}
