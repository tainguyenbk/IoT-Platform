using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Domain.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.Customers
{
    public class CustomerResponse
    {
        public Customer Customer { get; set; }
        public List<Device> Devices { get; set; } 
        public List<AuditLog> AuditLogs { get; set; }
    }
}
