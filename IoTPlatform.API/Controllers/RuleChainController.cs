using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RuleChainController : ControllerBase
    {
        private readonly IRuleChainService _ruleChainService;

        public RuleChainController(IRuleChainService ruleChainService)
        {
            _ruleChainService = ruleChainService;
        }

        [HttpPost]
        public async Task<ActionResult> AddRuleChain(RuleChain ruleChain)
        {
            var result = await _ruleChainService.AddRuleChainAsync(ruleChain);
            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRuleChains()
        {
            var result = await _ruleChainService.GetAllRuleChainsAsync();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindRuleChainByID(string id)
        {
            var result = await _ruleChainService.FindRuleChainByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRuleChain(string id, RuleChain ruleChain)
        {
            var result = await _ruleChainService.UpdateRuleChainAsync(id, ruleChain);
            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveRuleChain(string id)
        {
            var result = await _ruleChainService.RemoveRuleChainAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> FindRuleChainByName(string name)
        {
            var result = await _ruleChainService.FindRuleChainByNameAsync(name);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }
    }
}
