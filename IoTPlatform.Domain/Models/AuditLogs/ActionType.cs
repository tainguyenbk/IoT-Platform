using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.AuditLogs
{
    public enum ActionType
    {
        [Description("Create")]
        Create,
        [Description("Read")]
        Read,
        [Description("Update")]
        Update,
        [Description("Delete")]
        Delete,
    }
}
