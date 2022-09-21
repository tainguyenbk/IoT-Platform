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

    }
}
