using IoTPlatform.Domain.Models.AuditLog;
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
    }
}
