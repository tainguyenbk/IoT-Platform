using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SharedAttributeController : Controller
    {
        private readonly ISharedAttributeService _sharedAttributeService;

        public SharedAttributeController(ISharedAttributeService sharedAttributeService)
        {
            _sharedAttributeService = sharedAttributeService;
        }

        [HttpPost]
        public async Task<ActionResult> AddSharedAttribute(SharedAttribute sharedAttribute)
        {
            var result = await _sharedAttributeService.AddSharedAttributeAsync(sharedAttribute);
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
            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveSharedAttribute(string id)
        {
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
