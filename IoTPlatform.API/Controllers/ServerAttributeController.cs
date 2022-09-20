using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServerAttributeController : ControllerBase
    {
        private readonly IServerAttributeService _serverAttributeService;

        public ServerAttributeController(IServerAttributeService serverAttributeService)
        {
            _serverAttributeService = serverAttributeService;
        }

        [HttpPost]
        public async Task<ActionResult> AddServerAttribute(ServerAttribute serverAttribute)
        {
            var result = await _serverAttributeService.AddServerAttributeAsync(serverAttribute);
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
            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveServerAttribute(string id)
        {
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
