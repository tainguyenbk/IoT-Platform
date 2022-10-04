using IoTPlatform.Domain.Models.DeviceProfiles;
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

        public async Task<IEnumerable<DeviceProfile>> FindDeviceProifleByRuleChainID(string ruleChainID)
        {
            var filter = Builders<DeviceProfile>.Filter.Eq("RuleChainID", ruleChainID);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<DeviceProfile> MakeDeviceProfileDefaultAsync(string id)
        {
            var filter = Builders<DeviceProfile>.Filter.Eq("DeviceProfileID", id);
            var deviceProfile = await DbSet.Find(filter).ToListAsync();
            var newDeviceProfile = new DeviceProfile();

            foreach (var item in deviceProfile)
            {
                newDeviceProfile = new DeviceProfile()
                {
                    DeviceProfileID = item.DeviceProfileID,
                    CreatedTime = item.CreatedTime,
                    DeviceProfileName = item.DeviceProfileName,
                    RuleChainID = item.RuleChainID,
                    Images = item.Images,
                    Description = item.Description,
                    TransportType = item.TransportType,
                    Default = true
                };
            }

            await DbSet.ReplaceOneAsync(filter, newDeviceProfile);
            var res = await DbSet.Find(filter).SingleOrDefaultAsync();
            return res;
        }

        public async Task<DeviceProfile> UploadImage(string id, List<DeviceProfileImage> deviceProfileImage)
        {
            var filter = Builders<DeviceProfile>.Filter.Eq("DeviceProfileID", id);
            var deviceProfile = await DbSet.Find(filter).FirstOrDefaultAsync();

            if (deviceProfile == null)
            {
                return null;
            }

            if (deviceProfile.Images == null)
            {
                deviceProfile.Images = deviceProfileImage;
            }
            else
            {
                foreach (var image in deviceProfileImage)
                {
                    deviceProfile.Images.Add(image);
                }
            }

            await DbSet.FindOneAndReplaceAsync(filter, deviceProfile);
            return deviceProfile;
        }
    }
}
