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

        public async Task<IEnumerable<Device>> FindDeviceByCustomerID(string customerID)
        {
            var filter = Builders<Device>.Filter.Eq("CustomerID", customerID);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Device>> FindDeviceByDeviceProfileID(string deviceProfileID)
        {
            var filter = Builders<Device>.Filter.Eq("DeviceProfileID", deviceProfileID);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Device>> FindDeviceByName(string name)
        {
            var filter = Builders<Device>.Filter.Eq("DeviceName", name);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
