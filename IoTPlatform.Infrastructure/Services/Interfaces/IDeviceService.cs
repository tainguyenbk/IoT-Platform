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
        Task<IEnumerable<Device>> FindDeviceByDeviceProfileIDAsync(string deviceProfileID);
        Task<IEnumerable<Device>> FindDeviceByCustomerIDAsync(string customerID);
        Task<DeviceResponse> FindAllInDeviceByID(string id);
        Task<Device> MakeDevicePublicAsync(string id);
        Task<Device> MakeDevicePrivateAysnc(string id);
        Task<Device> AssignDeviceToCustomerAsync(string id, string customerID);
        Task<Device> UnAssignDeviceToCustomerAsync(string id);
    }
}
