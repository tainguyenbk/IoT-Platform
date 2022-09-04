using IoTPlatform.Domain.Models;
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
    public class ServerAttributeRepository : MongoRepository<ServerAttribute>, IServerAttributeRepository
    {
        public ServerAttributeRepository(IOptions<DatabaseSetting> databaseSetting) : base(databaseSetting)
        {

        }

        public async Task<IEnumerable<ServerAttribute>> FindServerAttributeByDeviceID(string deviceID)
        {
            var filter = Builders<ServerAttribute>.Filter.Eq("DeviceID", deviceID);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
