using IoTPlatform.Domain.Models.RuleChain;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories
{
    public class RuleChainRepository : MongoRepository<RuleChain>, IRuleChainRepository
    {
        public RuleChainRepository(IOptions<DatabaseSetting> databaseSetting) : base(databaseSetting)
        {

        }

        public async Task<IEnumerable<RuleChain>> FindRuleChainByName(string name)
        {
            var filter = Builders<RuleChain>.Filter.Eq("RuleChainName", name);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
