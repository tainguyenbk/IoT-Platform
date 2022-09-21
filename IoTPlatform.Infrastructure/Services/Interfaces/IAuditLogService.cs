using IoTPlatform.Domain.Models.AuditLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task<AuditLog> AddAnAuditLogAsync(DateTime time, string userName, string userId, string auditLogId, EntityType entityType, ActionType type);
    }
}
