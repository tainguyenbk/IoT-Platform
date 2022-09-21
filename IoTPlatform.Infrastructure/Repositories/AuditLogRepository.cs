using IoTPlatform.Domain.Models.AuditLog;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;
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
    }
}
