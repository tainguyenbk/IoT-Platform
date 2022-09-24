using IoTPlatform.Domain.Models.AuditLog;
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

        public async Task<AuditLog> AddAnAuditLogAsync(DateTime timeStamp, EntityType entityType, string entityID, string entityName, string userName, string userID, ActionType actionType)
        {
            var auditLog = new AuditLog(timeStamp, entityType, entityID, entityName, userName, userID, actionType);
            return await _auditLogRepository.Add(auditLog);
        }

        public async Task<IEnumerable<AuditLog>> GetAllAuditLogs()
        {
            return await _auditLogRepository.GetAll();
        }
    }
}
