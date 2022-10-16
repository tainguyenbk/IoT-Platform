using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Domain.Models.RuleChains;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RuleChainController : ControllerBase
    {
        private readonly IRuleChainService _ruleChainService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;
        private const string inValidID = "Rule Chain ID provided is not a valid ID";
        public RuleChainController(IRuleChainService ruleChainService, IUserService userService, IAuditLogService auditLogService)
        {
            _ruleChainService = ruleChainService;
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
        public async Task<ActionResult> AddRuleChain(RuleChain ruleChain)
        {
            var result = await _ruleChainService.AddRuleChainAsync(ruleChain);

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.RuleChain, result.RuleChainID,
                result.RuleChainName, userInfor[0], userInfor[1], ActionType.Create, status);

            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRuleChains()
        {
            var listRuleChain = await _ruleChainService.GetAllRuleChainsAsync();
            if (!listRuleChain.Any())
            {
                return NotFound();
            }

            var result = listRuleChain.OrderByDescending(o => o.RuleChain.CreatedTime);
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindRuleChainByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

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
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

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
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _ruleChainService.UpdateRuleChainAsync(id, ruleChain);
            if (result == null)
            {
                return NotFound();
            }    

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.RuleChain, result.RuleChainID,
                result.RuleChainName, userInfor[0], userInfor[1], ActionType.Update, status);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveRuleChain(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var obj = await _ruleChainService.FindRuleChainByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.RuleChain, obj.RuleChainID,
                obj.RuleChainName, userInfor[0], userInfor[1], ActionType.Delete, status);
           
            var result = await _ruleChainService.RemoveRuleChainAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> FindRuleChainByName(string name)
        {
            var listRuleChain = await _ruleChainService.FindRuleChainByNameAsync(name);
            if (!listRuleChain.Any())
            {
                return NotFound();
            }

            var result = listRuleChain.OrderByDescending(o => o.CreatedTime);
            return new JsonResult(new { result });
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> MakeRuleChainRoot(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _ruleChainService.MakeRuleChainRootAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.RuleChain, result.RuleChainID,
                result.RuleChainName, userInfor[0], userInfor[1], ActionType.MakeRoot, status);

            return new JsonResult(new { result });
        }
    }
}
