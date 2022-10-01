using IoTPlatform.Domain.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface ISharedAttributeService
    {
        Task<SharedAttribute> AddSharedAttributeAsync(SharedAttribute sharedAttribute);
        Task<SharedAttribute> FindSharedAttributeByIdAsync(string id);
        Task<IEnumerable<SharedAttribute>> GetAllSharedAttributesAsync();
        Task<SharedAttribute> UpdateSharedAttributeAsync(string id, SharedAttribute sharedAttribute);
        Task<bool> RemoveSharedAttributeAsync(string id);
        Task<IEnumerable<SharedAttribute>> FindSharedAttributeByDeviceIDAsync(string deviceID);
    }
}
