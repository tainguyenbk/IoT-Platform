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
        Task<Device> MakeDevicePublic(string id);
        Task<Device> MakeDevicePrivate(string id);
        Task<Device> AssignDeviceToCustomer(string id);
        Task<Device> UnAssignDeviceToCustomer(string id);
    }
}
