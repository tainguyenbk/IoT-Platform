using IoTPlatform.Domain.Models.DeviceProfiles;
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
        private readonly IAuditLogRepository _auditLogRepository;

        public DeviceProfileService(IDeviceProfileRepository deviceProfileRepository, IAuditLogRepository auditLogRepository)
        {
            _deviceProfileRepository = deviceProfileRepository;
            _auditLogRepository = auditLogRepository;
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

        public async Task<DeviceProfileResponse?> FindDeviceProfileDetailByIdAsync(string id)
        {
            var deviceProfile = await _deviceProfileRepository.GetById(id);

            if (deviceProfile != null)
            {
                var auditLogs = await _auditLogRepository.FindAuditLogsByEntityID(id);
                DeviceProfileResponse deviceProfileResponse = new DeviceProfileResponse()
                {
                    DeviceProfile = deviceProfile,
                    AudiLogs = auditLogs.ToList()
                };
                return deviceProfileResponse;
            }
            return null;
        }

        public Task<IEnumerable<DeviceProfile>> FindDeviceProifleByRuleChainIDAsync(string ruleChainID)
        {
            return _deviceProfileRepository.FindDeviceProifleByRuleChainID(ruleChainID);
        }

        public async Task<IEnumerable<DeviceProfileResponse>> GetAllDeviceProfilesAsync()
        {
            var result = new List<DeviceProfileResponse>();
            var listDeviceProfile = await _deviceProfileRepository.GetAll();
            
            foreach (var deviceProfile in listDeviceProfile)
            {
                var auditLogs = await _auditLogRepository.FindAuditLogsByEntityID(deviceProfile.DeviceProfileID);
                DeviceProfileResponse deviceProfileResponse = new DeviceProfileResponse()
                {
                    DeviceProfile = deviceProfile,
                    AudiLogs = auditLogs.ToList(),
                };
                result.Add(deviceProfileResponse);
            }
            return result;
        }

        public async Task<DeviceProfile> MakeDeviceProfileDefaultAsync(string id)
        {
            return await _deviceProfileRepository.MakeDeviceProfileDefaultAsync(id);
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
