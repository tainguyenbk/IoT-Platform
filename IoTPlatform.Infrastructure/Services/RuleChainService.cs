using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using IoTPlatform.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services
{
    public class RuleChainService : IRuleChainService
    {
        private readonly IRuleChainRepository _ruleChainRepository;

        public RuleChainService(IRuleChainRepository ruleChainRepository)
        {
            _ruleChainRepository = ruleChainRepository;
        }

        public Task<RuleChain> AddRuleChainAsync(RuleChain ruleChain)
        {
            return _ruleChainRepository.Add(ruleChain);
        }

        public Task<RuleChain> FindRuleChainByIdAsync(string id)
        {
            return _ruleChainRepository.GetById(id);
        }

        public Task<IEnumerable<RuleChain>> FindRuleChainByNameAsync(string name)
        {
            return _ruleChainRepository.FindRuleChainByName(name);
        }

        public Task<IEnumerable<RuleChain>> GetAllRuleChainsAsync()
        {
            return _ruleChainRepository.GetAll();
        }

        public Task<bool> RemoveRuleChainAsync(string id)
        {
            return _ruleChainRepository.Remove(id);
        }

        public Task<RuleChain> UpdateRuleChainAsync(string id, RuleChain ruleChain)
        {
            return _ruleChainRepository.Update(id, ruleChain);
        }
    }
}
