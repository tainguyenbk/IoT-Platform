using IoTPlatform.Domain.Models.Attribute;
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
    public class ClientAttributeRepository : MongoRepository<ClientAttribute>, IClientAttributeRepository
    {
        public ClientAttributeRepository(IOptions<DatabaseSetting> databaseSetting) : base(databaseSetting)
        {

        }

        public async Task<IEnumerable<ClientAttribute>> FindClientAttributeByDeviceID(string deviceID)
        {
            var filter = Builders<ClientAttribute>.Filter.Eq("DeviceID", deviceID);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
