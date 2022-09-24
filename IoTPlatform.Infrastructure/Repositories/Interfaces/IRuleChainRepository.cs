using IoTPlatform.Domain.Models.RuleChain;
using IoTPlatform.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories.Interfaces
{
    public interface IRuleChainRepository : IBaseRepository<RuleChain>
    {
        Task<IEnumerable<RuleChain>> FindRuleChainByName(string name);
    }
}
