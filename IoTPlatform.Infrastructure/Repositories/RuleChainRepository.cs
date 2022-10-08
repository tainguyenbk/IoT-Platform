using IoTPlatform.Domain.Models.RuleChains;
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

        public async Task<RuleChain> MakeRuleChainRoot(string id)
        {
            var filter = Builders<RuleChain>.Filter.Eq("RuleChainID", id);
            var ruleChain = await DbSet.Find(filter).ToListAsync();
            var newRuleChain = new RuleChain();

            foreach (var item in ruleChain)
            {
                newRuleChain = new RuleChain()
                {
                    RuleChainID = item.RuleChainID,
                    CreatedTime = item.CreatedTime,
                    RuleChainName = item.RuleChainName,
                    DebugMode = item.DebugMode,
                    Root = true,
                    Description = item.Description
                };
            }

            await DbSet.ReplaceOneAsync(filter, newRuleChain);
            var res = await DbSet.Find(filter).SingleOrDefaultAsync();
            return res;
        }
    }
}
