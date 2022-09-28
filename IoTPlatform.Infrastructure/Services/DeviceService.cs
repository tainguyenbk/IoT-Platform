using IoTPlatform.Domain.Models.Devices;
using IoTPlatform.Infrastructure.Repositories;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using IoTPlatform.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public Task<Device> AddDeviceAsync(Device device)
        {
            return _deviceRepository.Add(device);
        }

        public Task<IEnumerable<Device>> FindDeviceByCustomerAsync(string customer)
        {
            return _deviceRepository.FindDeviceByCustomer(customer);
        }

        public Task<IEnumerable<Device>> FindDeviceByDeviceProfileAsync(string deviceProfile)
        {
            return _deviceRepository.FindDeviceByDeviceProfile(deviceProfile);
        }

        public Task<Device> FindDeviceByIdAsync(string id)
        {
            return _deviceRepository.GetById(id);
        }

        public Task<IEnumerable<Device>> FindDeviceByLabelAsync(string label)
        {
            return _deviceRepository.FindDeviceByLabel(label);
        }

        public Task<IEnumerable<Device>> FindDeviceByNameAsync(string name)
        {
            return _deviceRepository.FindDeviceByName(name);
        }

        public Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return _deviceRepository.GetAll();
        }

        public Task<bool> RemoveDeviceAsync(string id)
        {
            return _deviceRepository.Remove(id);
        }

        public Task<Device> UpdateDeviceAsync(string id, Device device)
        {
            return _deviceRepository.Update(id, device);
        }
    }
}
