using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.AuditLogs
{
    public enum EntityType
    {
        [Description("Device")]
        Device,
        [Description("Device Profile")]
        DeviceProfile,
        [Description("Customer")]
        Custormer,
        [Description("Rule Chain")]
        RuleChain,
        [Description("Client Attribute")]
        ClientAttribute,
        [Description("Server Attribute")]
        ServerAttribute,
        [Description("Shared Attribute")]
        SharedAttribute,
        [Description("Telemetry")]
        Telemetry,
    }
}
