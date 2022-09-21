using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.AuditLog
{
    public enum EntityType
    {
        Device, 
        DeviceProfile,
        Custormer, 
        RuleChain,
        ClientAttribute, 
        ServerAttribute,
        SharedAttribute,
        Telemetry,
        Alarm
    }
}
