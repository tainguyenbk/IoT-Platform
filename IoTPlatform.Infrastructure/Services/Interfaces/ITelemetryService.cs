using IoTPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface ITelemetryService
    {
        Task<Telemetry> AddTelemetryAsync(Telemetry telemetry);
        Task<Telemetry> FindTelemetryByIdAsync(string id);
        Task<IEnumerable<Telemetry>> GetAllTelemetrysAsync();
        Task<Telemetry> UpdateTelemetryAsync(string id, Telemetry telemetry);
        Task<bool> RemoveTelemetryAsync(string id);
        Task<IEnumerable<Telemetry>> FindTelemetryByDeviceIDAsync(string deviceID);
    }
}
