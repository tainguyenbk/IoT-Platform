using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories
{
    public class AuditLogRepository : MongoRepository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(IOptions<DatabaseSetting> databaseSetting) : base(databaseSetting)
        {
        }

        public async Task<IEnumerable<AuditLog>> FindAuditLogByEntityTypeName(string entityTypeName)
        {
            var filter = Builders<AuditLog>.Filter.Eq("EntityTypeName", entityTypeName);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<AuditLog>> FindAuditLogByUserName(string userName)
        {
            var filter = Builders<AuditLog>.Filter.Eq("UserName", userName);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<AuditLog>> FindAuditLogInPeriodTime(DateTime startTime, DateTime endTime)
        {
            var filter = Builders<AuditLog>.Filter.Gte("TimeStamp", startTime);
            filter &= Builders<AuditLog>.Filter.Lte("TimeStamp", endTime);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<AuditLog>> FindAuditLogs(string? auditLogID, string? entityTypeName, string? userName, DateTime? startTime, DateTime? endTime)
        {
            var filter = Builders<AuditLog>.Filter.Empty;

            if (auditLogID != null)
            {
                filter &= Builders<AuditLog>.Filter.Eq("AuditLogID", auditLogID);
            }

            if (entityTypeName != null)
            {
                filter &= Builders<AuditLog>.Filter.Eq("EntityTypeName", entityTypeName);
            }

            if (userName != null)
            {
                filter &= Builders<AuditLog>.Filter.Eq("UserName", userName);
            }

            if (startTime != null)
            {
                filter &= Builders<AuditLog>.Filter.Gte("TimeStamp", startTime);
            }

            if (endTime != null)
            {
                filter &= Builders<AuditLog>.Filter.Lte("TimeStamp", endTime);
            }

            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }

        public async Task<IEnumerable<AuditLog>> FindAuditLogsByEntityID(string entityID)
        {
            var filter = Builders<AuditLog>.Filter.Eq("EntityID", entityID);
            var res = await DbSet.Find(filter).ToListAsync();
            return res;
        }
    }
}
