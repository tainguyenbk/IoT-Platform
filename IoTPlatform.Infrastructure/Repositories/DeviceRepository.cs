using IoTPlatform.Domain.Models.Devices;
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
    public class DeviceRepository : MongoRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(IOptions<DatabaseSetting> databaseSetting) : base(databaseSetting)
        {

        }

        public async Task<IEnumerable<Device>> FindDeviceByCustomer(string customer)
        {
            var filter = Builders<Device>.Filter.Eq("Customer", customer);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Device>> FindDeviceByDeviceProfile(string deviceProfile)
        {
            var filter = Builders<Device>.Filter.Eq("DeviceProfile", deviceProfile);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Device>> FindDeviceByLabel(string label)
        {
            var filter = Builders<Device>.Filter.Eq("Label", label);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Device>> FindDeviceByName(string name)
        {
            var filter = Builders<Device>.Filter.Eq("Name", name);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
