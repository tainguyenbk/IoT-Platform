using IoTPlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface IDeviceProfileService
    {
        Task<DeviceProfile> AddDeviceProfileAsync(DeviceProfile deviceProfile);
        Task<DeviceProfile> FindDeviceProfileByIdAsync(string id);
        Task<IEnumerable<DeviceProfile>> GetAllDeviceProfilesAsync();
        Task<DeviceProfile> UpdateDeviceProfleAsync(string id, DeviceProfile deviceProfile);
        Task<bool> RemoveDeviceProfileAsync(string id);
        Task<IEnumerable<DeviceProfile>> FindDeviceProfileByNameAsync(string name);
        Task<DeviceProfile> UploadImageAsync(string id, List<DeviceProfileImage> images);
    }
}
