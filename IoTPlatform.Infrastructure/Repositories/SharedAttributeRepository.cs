using IoTPlatform.Domain.Models.Attributes;
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
    public class SharedAttributeRepository : MongoRepository<SharedAttribute>, ISharedAttributeRepository
    {
        public SharedAttributeRepository(IOptions<DatabaseSetting> databaseSetting) : base(databaseSetting)
        {

        }

        public async Task<IEnumerable<SharedAttribute>> FindSharedAttributeByDeviceID(string deviceID)
        {
            var filter = Builders<SharedAttribute>.Filter.Eq("DeviceID", deviceID);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
