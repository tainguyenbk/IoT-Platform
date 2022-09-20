using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using IoTPlatform.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly ITelemetryRepository _telemetryRepository;

        public TelemetryService(ITelemetryRepository telemetryRepository)
        {
            _telemetryRepository = telemetryRepository;
        }

        public Task<Telemetry> AddTelemetryAsync(Telemetry telemetry)
        {
            return _telemetryRepository.Add(telemetry); 
        }

        public Task<IEnumerable<Telemetry>> FindTelemetryByDeviceIDAsync(string deviceID)
        {
            return _telemetryRepository.FindTelemetryByDeviceID(deviceID);
        }

        public Task<Telemetry> FindTelemetryByIdAsync(string id)
        {
            return _telemetryRepository.GetById(id);
        }

        public Task<IEnumerable<Telemetry>> GetAllTelemetrysAsync()
        {
            return _telemetryRepository.GetAll();
        }

        public Task<bool> RemoveTelemetryAsync(string id)
        {
            return _telemetryRepository.Remove(id);
        }

        public Task<Telemetry> UpdateTelemetryAsync(string id, Telemetry telemetry)
        {
            return _telemetryRepository.Update(id, telemetry);
        }
    }
}
