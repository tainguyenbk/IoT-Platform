using IoTPlatform.Domain.Models.RuleChains;
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
        private readonly IAuditLogRepository _auditLogRepository;

        public RuleChainService(IRuleChainRepository ruleChainRepository, IAuditLogRepository auditLogRepository)
        {
            _ruleChainRepository = ruleChainRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<RuleChain> AddRuleChainAsync(RuleChain ruleChain)
        {
            return await _ruleChainRepository.Add(ruleChain);
        }

        public async Task<RuleChain> FindRuleChainByIdAsync(string id)
        {
            return await _ruleChainRepository.GetById(id);
        }

        public async Task<IEnumerable<RuleChain>> FindRuleChainByNameAsync(string name)
        {
            return await _ruleChainRepository.FindRuleChainByName(name);
        }

        public async Task<RuleChainResponse> FindRuleChainDetailByIdAsync(string id)
        {
            var ruleChain = await _ruleChainRepository.GetById(id);

            if (ruleChain != null)
            {
                var auditLogs = await _auditLogRepository.FindAuditLogsByEntityID(ruleChain.RuleChainID);

                RuleChainResponse ruleChainResponse = new RuleChainResponse()
                {
                    RuleChain = ruleChain,
                    AuditLogs = auditLogs.ToList()
                };
                return ruleChainResponse;
            }
            return null;
        }

        public async Task<IEnumerable<RuleChainResponse>> GetAllRuleChainsAsync()
        {
            var result = new List<RuleChainResponse>();
            var listRuleChain = await _ruleChainRepository.GetAll();

            foreach (var ruleChain in listRuleChain)
            {
                var auditLogs = await _auditLogRepository.FindAuditLogsByEntityID(ruleChain.RuleChainID);

                RuleChainResponse ruleChainResponse = new RuleChainResponse()
                {
                    RuleChain = ruleChain,
                    AuditLogs = auditLogs.ToList()
                };
                result.Add(ruleChainResponse);
            }
            return result;
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
