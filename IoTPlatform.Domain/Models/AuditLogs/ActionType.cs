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
        [Description("Update")]
        Update,
        [Description("Delete")]
        Delete,
        [Description("Make public")]
        MakePublic,
        [Description("Make private")]
        MakePrivate,
        [Description("Assign to Customer")]
        AssignCustomer,
        [Description("Unassign to Customer")]
        UnAssignCustomer,
        [Description("Make default")]
        MakeDefault,
        [Description("Make root")]
        MakeRoot,
        [Description("Create Attribute")]
        CreateAttribute,
        [Description("Update Attribute")]
        UpdateAttribute,
        [Description("Delete Attribute")]
        DeleteAttribute,
    }
}
