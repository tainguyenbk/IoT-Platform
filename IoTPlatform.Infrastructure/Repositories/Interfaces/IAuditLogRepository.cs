using IoTPlatform.Domain.Models.AuditLog;
using IoTPlatform.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories.Interfaces
{
    public interface IAuditLogRepository : IBaseRepository<AuditLog>
    {
        Task<IEnumerable<AuditLog>> FindAuditLogByEntityTypeName(string entityTypeName);
        Task<IEnumerable<AuditLog>> FindAuditLogByUserName(string userName);
        Task<IEnumerable<AuditLog>> FindAuditLogInPeriodTime(DateTime startTime, DateTime endTime);
        Task<IEnumerable<AuditLog>> FindAuditLogs(string? auditLogID, string? entityTypeName, string? userName, DateTime? startTime, DateTime? endTime);
    }
}
