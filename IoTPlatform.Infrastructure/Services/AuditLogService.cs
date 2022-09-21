using IoTPlatform.Domain.Models.AuditLog;
using IoTPlatform.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        public Task<AuditLog> AddAnAuditLogAsync(DateTime time, string userName, string userId, string auditLogId, EntityType entityType, ActionType type)
        {
            throw new NotImplementedException();
        }
    }
}
