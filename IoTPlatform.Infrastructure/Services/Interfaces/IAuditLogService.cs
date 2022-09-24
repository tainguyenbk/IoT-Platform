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
        Task<AuditLog> FindAuditLogByIdAsync(string id);
        Task<IEnumerable<AuditLog>> FindAuditLogByEntityTypeNameAsync(string entityTypeName);
        Task<IEnumerable<AuditLog>> FindAuditLogByUserNameAsync(string userName);
        Task<IEnumerable<AuditLog>> FindAuditLogInPeriodTimeAsync(DateTime startTime, DateTime endTime);
    }
}
