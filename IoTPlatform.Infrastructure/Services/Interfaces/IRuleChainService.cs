using IoTPlatform.Domain.Models.RuleChains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface IRuleChainService
    {
        Task<RuleChain> AddRuleChainAsync(RuleChain ruleChain);
        Task<RuleChain> FindRuleChainByIdAsync(string id);
        Task<IEnumerable<RuleChain>> GetAllRuleChainsAsync();
        Task<RuleChain> UpdateRuleChainAsync(string id, RuleChain ruleChain);
        Task<bool> RemoveRuleChainAsync(string id);
        Task<IEnumerable<RuleChain>> FindRuleChainByNameAsync(string name);
    }
}
