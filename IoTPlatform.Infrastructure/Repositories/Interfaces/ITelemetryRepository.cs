using IoTPlatform.Domain.Models.Telemetry;
using IoTPlatform.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories.Interfaces
{
    public interface ITelemetryRepository : IBaseRepository<Telemetry>
    {
        Task<IEnumerable<Telemetry>> FindTelemetryByDeviceID(string deviceID);
    }
}
