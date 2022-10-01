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
        private readonly IClientAttributeRepository _clientAttributeRepository;
        private readonly IServerAttributeRepository _serverAttributeRepository;
        private readonly ISharedAttributeRepository _sharedAttributeRepository;
        private readonly ITelemetryRepository _telemetryRepository;
        private readonly IAuditLogRepository _auditLogRepository;

        public DeviceService(IDeviceRepository deviceRepository, IClientAttributeRepository clientAttributeRepository, 
            IServerAttributeRepository serverAttributeRepository, ISharedAttributeRepository sharedAttributeRepository,
            ITelemetryRepository telemetryRepository, IAuditLogRepository auditLogRepository)
        {
            _deviceRepository = deviceRepository;
            _clientAttributeRepository = clientAttributeRepository;
            _serverAttributeRepository = serverAttributeRepository;
            _sharedAttributeRepository = sharedAttributeRepository;
            _telemetryRepository = telemetryRepository;
            _auditLogRepository = auditLogRepository;
        }

        public Task<Device> AddDeviceAsync(Device device)
        {
            return _deviceRepository.Add(device);
        }

        public async Task<Device> AssignDeviceToCustomerAsync(string id, string customerID)
        {
            return await _deviceRepository.AssignToCustomer(id, customerID);
        }

        public async Task<DeviceResponse?> FindAllInDeviceByID(string id)
        {
            var device = await _deviceRepository.GetById(id);

            if (device == null)
            {
                return null;
            }    
            else
            {
                var clientAttributes = await _clientAttributeRepository.FindClientAttributeByDeviceID(device.DeviceID);
                var serverAttributes = await _serverAttributeRepository.FindServerAttributeByDeviceID(device.DeviceID);
                var sharedAttributes = await _sharedAttributeRepository.FindSharedAttributeByDeviceID(device.DeviceID);
                var lastTelemetry = await _telemetryRepository.FindLastestTelemetry();
                var auditLogs = await _auditLogRepository.FindAuditLogsByEntityID(device.DeviceID);

                DeviceResponse deviceResponse = new DeviceResponse()
                {
                    Device = device,
                    ClientAttributes = clientAttributes.ToList(),
                    ServerAttributes = serverAttributes.ToList(),
                    SharedAttributes = sharedAttributes.ToList(),
                    LastTelemetry = lastTelemetry,
                    AuditLogs = auditLogs.ToList()
                };
                return deviceResponse;
            }
        }

        public Task<IEnumerable<Device>> FindDeviceByCustomerIDAsync(string customer)
        {
            return _deviceRepository.FindDeviceByCustomerID(customer);
        }

        public Task<IEnumerable<Device>> FindDeviceByDeviceProfileIDAsync(string deviceProfile)
        {
            return _deviceRepository.FindDeviceByDeviceProfileID(deviceProfile);
        }

        public Task<Device> FindDeviceByIdAsync(string id)
        {
            return _deviceRepository.GetById(id);
        }

        public Task<IEnumerable<Device>> FindDeviceByNameAsync(string name)
        {
            return _deviceRepository.FindDeviceByName(name);
        }

        public async Task<IEnumerable<DeviceResponse>> GetAllDevicesAsync()
        {
            var result = new List<DeviceResponse>();
            var listDevice = await _deviceRepository.GetAll();

            foreach (var device in listDevice)
            {
                var clientAttributes = await _clientAttributeRepository.FindClientAttributeByDeviceID(device.DeviceID);
                var serverAttributes = await _serverAttributeRepository.FindServerAttributeByDeviceID(device.DeviceID);
                var sharedAttributes = await _sharedAttributeRepository.FindSharedAttributeByDeviceID(device.DeviceID);
                var lastTelemetry = await _telemetryRepository.FindLastestTelemetry();
                var auditLogs = await _auditLogRepository.FindAuditLogsByEntityID(device.DeviceID);

                DeviceResponse deviceResponse = new DeviceResponse()
                {
                    Device = device,
                    ClientAttributes = clientAttributes.ToList(),
                    ServerAttributes = serverAttributes.ToList(),
                    SharedAttributes = sharedAttributes.ToList(),
                    LastTelemetry = lastTelemetry,
                    AuditLogs = auditLogs.ToList()
                };
                result.Add(deviceResponse);
            }
            return result;
        }

        public async Task<Device> MakeDevicePrivateAysnc(string id)
        {
            return await _deviceRepository.MakeDevicePrivate(id);
        }

        public async Task<Device> MakeDevicePublicAsync(string id)
        {
            return await _deviceRepository.MakeDevicePublic(id);
        }

        public Task<bool> RemoveDeviceAsync(string id)
        {
            return _deviceRepository.Remove(id);
        }

        public Task<Device> UnAssignDeviceToCustomerAsync(string id)
        {
            return _deviceRepository.UnAssignToCustomer(id);
        }

        public Task<Device> UpdateDeviceAsync(string id, Device device)
        {
            return _deviceRepository.Update(id, device);
        }
    }
}
