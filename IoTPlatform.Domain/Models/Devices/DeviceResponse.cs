using IoTPlatform.Domain.Models.Attributes;
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
        public string? DeviceID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string DeviceName { get; set; }
        public string DeviceProfileID { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }
        public string? CustomerID { get; set; }
        public bool? Public { get; set; }
        public bool? IsGateway { get; set; }
        public List<ClientAttribute>? ClientAttributes { get; set; }
        public List<ServerAttribute>? ServerAttributes { get; set; }
        public List<SharedAttribute>? SharedAttributes { get; set; }
    }
}
