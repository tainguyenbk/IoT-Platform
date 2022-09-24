using IoTPlatform.Domain.Models.Attribute;
using IoTPlatform.Domain.Models.AuditLog;
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

        [HttpPost]
        public async Task<ActionResult> AddServerAttribute(ServerAttribute serverAttribute)
        {
            var result = await _serverAttributeService.AddServerAttributeAsync(serverAttribute);

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.ServerAttribute, result.AttributeID,
                "", userInfor[0], userInfor[1], ActionType.Create);

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
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.ServerAttribute, result.AttributeID,
                "", userInfor[0], userInfor[1], ActionType.Update);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveServerAttribute(string id)
        {   
            var removeAttribute = await _serverAttributeService.FindServerAttributeByIdAsync(id);
            if (removeAttribute == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.ServerAttribute, removeAttribute.AttributeID,
                "", userInfor[0], userInfor[1], ActionType.Create);

            var result = await _serverAttributeService.RemoveServerAttributeAsync(id);
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
