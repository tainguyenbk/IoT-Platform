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
        Task<AuditLog> AddAnAuditLogAsync(DateTime timeStamp, EntityType entityType, string entityID, string entityName, string userName, string userID, ActionType actionType);
        Task<IEnumerable<AuditLog>> GetAllAuditLogs();
    }
}
