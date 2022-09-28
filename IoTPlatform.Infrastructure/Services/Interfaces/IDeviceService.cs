using IoTPlatform.Domain.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<Device> AddDeviceAsync(Device device);
        Task<Device> FindDeviceByIdAsync(string id);
        Task<IEnumerable<DeviceResponse>> GetAllDevicesAsync();
        Task<Device> UpdateDeviceAsync(string id, Device device);
        Task<bool> RemoveDeviceAsync(string id);
        Task<IEnumerable<Device>> FindDeviceByNameAsync(string name);
        Task<IEnumerable<Device>> FindDeviceByDeviceProfileAsync(string deviceProfile);
        Task<IEnumerable<Device>> FindDeviceByLabelAsync(string label);
        Task<IEnumerable<Device>> FindDeviceByCustomerAsync(string customer);
    }
}
