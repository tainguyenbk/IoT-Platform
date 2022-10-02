using IoTPlatform.Domain.Models.AuditLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.DeviceProfiles
{
    public class DeviceProfileResponse
    {
        DeviceProfile? DeviceProfile { get; set; }
        List<AuditLog>? AudiLogs { get; set; }
    }
}
