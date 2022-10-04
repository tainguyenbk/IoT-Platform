using IoTPlatform.Domain.Models.AuditLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.RuleChains
{
    public class RuleChainResponse
    {
        public RuleChain RuleChain { get; set; } 
        public List<AuditLog> AuditLogs { get; set; }
    }
}
