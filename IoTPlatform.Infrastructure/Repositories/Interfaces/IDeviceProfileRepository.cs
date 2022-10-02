using IoTPlatform.Domain.Models.DeviceProfiles;
using IoTPlatform.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories.Interfaces
{
    public interface IDeviceProfileRepository : IBaseRepository<DeviceProfile>
    {
        Task<IEnumerable<DeviceProfile>> FindDeviceProifleByName(string name);
        Task<IEnumerable<DeviceProfile>> FindDeviceProifleByRuleChainID(string ruleChainID);
        Task<DeviceProfile> UploadImage(string id, List<DeviceProfileImage> deviceProfileImage);
    }
}
