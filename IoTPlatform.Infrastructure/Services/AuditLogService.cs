using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
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
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AuditLog> AddAnAuditLogAsync(DateTime timeStamp, EntityType entityType, string entityID, string entityName, string userName, string userID, ActionType actionType, string status)
        {
            var auditLog = new AuditLog(timeStamp, entityType, entityID, entityName, userName, userID, actionType, status);
            return await _auditLogRepository.Add(auditLog);
        }

        public async Task<IEnumerable<AuditLog>> FindAuditLogByEntityTypeNameAsync(string entityTypeName)
        {
            return await _auditLogRepository.FindAuditLogByEntityTypeName(entityTypeName);
        }

        public async Task<AuditLog> FindAuditLogByIdAsync(string id)
        {
            return await _auditLogRepository.GetById(id);
        }

        public async Task<IEnumerable<AuditLog>> FindAuditLogByUserNameAsync(string userName)
        {
            return await _auditLogRepository.FindAuditLogByUserName(userName);
        }

        public async Task<IEnumerable<AuditLog>> FindAuditLogInPeriodTimeAsync(DateTime startTime, DateTime endTime)
        {
            return await _auditLogRepository.FindAuditLogInPeriodTime(startTime, endTime);
        }

        public async Task<IEnumerable<AuditLog>> FindAuditLogsAsync(string? auditLogID, string? entityTypeName, string? userName, DateTime? startTime, DateTime? endTime)
        {
            return await _auditLogRepository.FindAuditLogs(auditLogID, entityTypeName, userName, startTime, endTime);
        }

        public async Task<IEnumerable<AuditLog>> GetAllAuditLogs()
        {
            return await _auditLogRepository.GetAll();
        }
    }
}
