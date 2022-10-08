using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Domain.Models.RuleChains;
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
                result.RuleChainName, userInfor[0], userInfor[1], ActionType.Create, "");

            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRuleChains()
        {
            var result = await _ruleChainService.GetAllRuleChainsAsync();
            if (!result.Any())
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

        [HttpGet("{id}")]
        public async Task<ActionResult> FindRuleChainDetailByID(string id)
        {
            var result = await _ruleChainService.FindRuleChainDetailByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRuleChain(string id, RuleChain ruleChain)
        {
            var obj = await _ruleChainService.FindRuleChainByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            var result = await _ruleChainService.UpdateRuleChainAsync(id, ruleChain);

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.RuleChain, result.RuleChainID,
                result.RuleChainName, userInfor[0], userInfor[1], ActionType.Update,"");

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveRuleChain(string id)
        { 
            var obj = await _ruleChainService.FindRuleChainByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.RuleChain, obj.RuleChainID,
                obj.RuleChainName, userInfor[0], userInfor[1], ActionType.Delete,"");
           
            var result = await _ruleChainService.RemoveRuleChainAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> FindRuleChainByName(string name)
        {
            var result = await _ruleChainService.FindRuleChainByNameAsync(name);
            if (!result.Any())
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> MakeRuleChainRoot(string id)
        {
            var obj = await _ruleChainService.FindRuleChainByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            var result = await _ruleChainService.MakeRuleChainRootAsync(id);
            return new JsonResult(new { result });
        }
    }
}
