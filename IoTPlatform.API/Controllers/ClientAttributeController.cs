using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientAttributeController : Controller
    {
        private readonly IClientAttributeService _clientAttributeService;

        public ClientAttributeController(IClientAttributeService clientAttributeService)
        {
            _clientAttributeService = clientAttributeService;
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
            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveClientAttribute(string id)
        {
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
