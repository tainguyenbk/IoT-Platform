using IoTPlatform.Domain.Models.Attributes;
using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Domain.Models.Telemetries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.Devices
{
    public class DeviceResponse
    {
        public Device? Device { get; set; }
        public List<ClientAttribute>? ClientAttributes { get; set; }
        public List<ServerAttribute>? ServerAttributes { get; set; }
        public List<SharedAttribute>? SharedAttributes { get; set; }
        public Telemetry? LastTelemetry { get; set; }
        public List<AuditLog>? AuditLogs { get; set; }
    }
}
