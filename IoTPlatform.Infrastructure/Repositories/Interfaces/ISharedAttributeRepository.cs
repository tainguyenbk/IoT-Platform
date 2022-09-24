using IoTPlatform.Domain.Models.Attribute;
using IoTPlatform.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories.Interfaces
{
    public interface ISharedAttributeRepository : IBaseRepository<SharedAttribute>
    {
        Task<IEnumerable<SharedAttribute>> FindSharedAttributeByDeviceID(string deviceID);
    }
}
