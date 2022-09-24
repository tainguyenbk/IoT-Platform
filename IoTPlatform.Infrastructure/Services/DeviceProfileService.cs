using IoTPlatform.Domain.Models.Device;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using IoTPlatform.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services
{
    public class DeviceProfileService : IDeviceProfileService
    {
        private readonly IDeviceProfileRepository _deviceProfileRepository;

        public DeviceProfileService(IDeviceProfileRepository deviceProfileRepository)
        {
            _deviceProfileRepository = deviceProfileRepository;
        }

        public Task<DeviceProfile> AddDeviceProfileAsync(DeviceProfile deviceProfile)
        {
            return _deviceProfileRepository.Add(deviceProfile);
        }

        public Task<DeviceProfile> FindDeviceProfileByIdAsync(string id)
        {
            return _deviceProfileRepository.GetById(id);
        }

        public Task<IEnumerable<DeviceProfile>> FindDeviceProfileByNameAsync(string name)
        {
            return _deviceProfileRepository.FindDeviceProifleByName(name);
        }

        public Task<IEnumerable<DeviceProfile>> GetAllDeviceProfilesAsync()
        {
            return _deviceProfileRepository.GetAll();
        }

        public Task<bool> RemoveDeviceProfileAsync(string id)
        {
            return _deviceProfileRepository.Remove(id);
        }

        public Task<DeviceProfile> UpdateDeviceProfleAsync(string id, DeviceProfile deviceProfile)
        {
            return _deviceProfileRepository.Update(id, deviceProfile);
        }

        public Task<DeviceProfile> UploadImageAsync(string id, List<DeviceProfileImage> deviceProfileImage)
        {
            return _deviceProfileRepository.UploadImage(id, deviceProfileImage);
        }
    }
}
