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
    public class DeviceProfileRepository : MongoRepository<DeviceProfile>, IDeviceProfileRepository
    {
        public DeviceProfileRepository(IOptions<DatabaseSetting> databaseSetting) : base(databaseSetting)
        {

        }

        public async Task<IEnumerable<DeviceProfile>> FindDeviceProifleByName(string name)
        {
            var filter = Builders<DeviceProfile>.Filter.Eq("DeviceProfileName", name);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
