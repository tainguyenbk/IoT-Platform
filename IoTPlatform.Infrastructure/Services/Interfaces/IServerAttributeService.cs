using IoTPlatform.Domain.Models.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface IServerAttributeService
    {
        Task<ServerAttribute> AddServerAttributeAsync(ServerAttribute serverAttribute);
        Task<ServerAttribute> FindServerAttributeByIdAsync(string id);
        Task<IEnumerable<ServerAttribute>> GetAllServerAttributesAsync();
        Task<ServerAttribute> UpdateServerAttributeAsync(string id, ServerAttribute serverAttribute);
        Task<bool> RemoveServerAttributeAsync(string id);
        Task<IEnumerable<ServerAttribute>> FindServerAttributeByDeviceIDAsync (string deviceID);
    }
}
