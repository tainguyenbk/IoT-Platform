using IoTPlatform.Domain.Models.AuditLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task<AuditLog> AddAnAuditLogAsync(DateTime timeStamp, EntityType entityType, string entityID, string entityName, string userName, string userID, ActionType actionType, string status);
        Task<IEnumerable<AuditLog>> GetAllAuditLogs();
        Task<AuditLog> FindAuditLogByIdAsync(string id);
        Task<IEnumerable<AuditLog>> FindAuditLogByEntityTypeNameAsync(string entityTypeName);
        Task<IEnumerable<AuditLog>> FindAuditLogByUserNameAsync(string userName);
        Task<IEnumerable<AuditLog>> FindAuditLogInPeriodTimeAsync(DateTime startTime, DateTime endTime);
        Task<IEnumerable<AuditLog>> FindAuditLogsAsync(string? auditLogID, string? entityTypeName, string? userName, DateTime? startTime, DateTime? endTime);
    }
}
