using IoTPlatform.Domain.Models;
using IoTPlatform.Domain.Models.AuditLog;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RuleChainController : ControllerBase
    {
        private readonly IRuleChainService _ruleChainService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;

        public RuleChainController(IRuleChainService ruleChainService, IUserService userService, IAuditLogService auditLogService)
        {
            _ruleChainService = ruleChainService;
            _userService = userService;
            _auditLogService = auditLogService;
        }

        [HttpPost]
        public async Task<ActionResult> AddRuleChain(RuleChain ruleChain)
        {
            var result = await _ruleChainService.AddRuleChainAsync(ruleChain);

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.RuleChain, result.RuleChainID,
                result.RuleChainName, userInfor[0], userInfor[1], ActionType.Create);

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

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.RuleChain, result.RuleChainID,
                result.RuleChainName, userInfor[0], userInfor[1], ActionType.Update);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveRuleChain(string id)
        {
            var result = await _ruleChainService.RemoveRuleChainAsync(id);

            var removeRuleChain = await _ruleChainService.FindRuleChainByIdAsync(id);
            if (removeRuleChain == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.RuleChain, removeRuleChain.RuleChainID,
                removeRuleChain.RuleChainName, userInfor[0], userInfor[1], ActionType.Delete);

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
