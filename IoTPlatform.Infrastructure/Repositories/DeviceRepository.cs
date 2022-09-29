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

        public async Task<Device> AssignToCustomer(string id, string customerID)
        {
            var filter = Builders<Device>.Filter.Eq("DeviceID", id);
            var device = await DbSet.Find(filter).ToListAsync();
            var newDevice = new Device();

            foreach (var item in device)
            {
                newDevice = new Device()
                {
                    DeviceID = item.DeviceID,
                    CreatedTime = item.CreatedTime,
                    DeviceName = item.DeviceName,
                    DeviceProfileID = item.DeviceProfileID,
                    Label = item.Label,
                    Description = item.Description,
                    CustomerID = customerID,
                    Public = item.Public,
                    IsGateway = item.IsGateway
                };
            }

            await DbSet.ReplaceOneAsync(filter, newDevice);
            var res = await DbSet.Find(filter).SingleOrDefaultAsync();
            return res;
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

        public async Task<Device> MakeDevicePrivate(string id)
        {
            var filter = Builders<Device>.Filter.Eq("DeviceID", id);
            var device = await DbSet.Find(filter).ToListAsync();
            var newDevice = new Device();

            foreach (var item in device)
            {
                newDevice = new Device()
                {
                    DeviceID = item.DeviceID,
                    CreatedTime = item.CreatedTime,
                    DeviceName = item.DeviceName,
                    DeviceProfileID = item.DeviceProfileID,
                    Label = item.Label,
                    Description = item.Description,
                    CustomerID = item.CustomerID,
                    Public = false,
                    IsGateway = item.IsGateway
                };
            }

            await DbSet.ReplaceOneAsync(filter, newDevice);
            var res = await DbSet.Find(filter).SingleOrDefaultAsync();
            return res;
        }

        public async Task<Device> MakeDevicePublic(string id)
        {
            var filter = Builders<Device>.Filter.Eq("DeviceID", id);
            var device = await DbSet.Find(filter).ToListAsync();
            var newDevice = new Device();

            foreach(var item in device)
            {
                newDevice = new Device()
                {
                    DeviceID = item.DeviceID,
                    CreatedTime = item.CreatedTime,
                    DeviceName = item.DeviceName,
                    DeviceProfileID = item.DeviceProfileID,
                    Label = item.Label,
                    Description = item.Description,
                    CustomerID = item.CustomerID,
                    Public = true,
                    IsGateway = item.IsGateway
                };
            }    

            await DbSet.ReplaceOneAsync(filter, newDevice);
            var res = await DbSet.Find(filter).SingleOrDefaultAsync();
            return res;
        }

        public async Task<Device> UnAssignToCustomer(string id)
        {
            var filter = Builders<Device>.Filter.Eq("DeviceID", id);
            var device = await DbSet.Find(filter).ToListAsync();
            var newDevice = new Device();

            foreach (var item in device)
            {
                newDevice = new Device()
                {
                    DeviceID = item.DeviceID,
                    CreatedTime = item.CreatedTime,
                    DeviceName = item.DeviceName,
                    DeviceProfileID = item.DeviceProfileID,
                    Label = item.Label,
                    Description = item.Description,
                    CustomerID = null,
                    Public = item.Public,
                    IsGateway = item.IsGateway
                };
            }

            await DbSet.ReplaceOneAsync(filter, newDevice);
            var res = await DbSet.Find(filter).SingleOrDefaultAsync();
            return res;
        }
    }
}
