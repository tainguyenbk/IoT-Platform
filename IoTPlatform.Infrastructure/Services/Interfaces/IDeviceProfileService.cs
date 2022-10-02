﻿using IoTPlatform.Domain.Models.DeviceProfiles;
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
        Task<DeviceProfileResponse> FindDeviceProfileDetailByIdAsync(string id);
        Task<IEnumerable<DeviceProfileResponse>> GetAllDeviceProfilesAsync();
        Task<DeviceProfile> UpdateDeviceProfleAsync(string id, DeviceProfile deviceProfile);
        Task<bool> RemoveDeviceProfileAsync(string id);
        Task<IEnumerable<DeviceProfile>> FindDeviceProfileByNameAsync(string name);
        Task<IEnumerable<DeviceProfile>> FindDeviceProifleByRuleChainIDAsync(string ruleChainID);
        Task<DeviceProfile> UploadImageAsync(string id, List<DeviceProfileImage> images);
    }
}
