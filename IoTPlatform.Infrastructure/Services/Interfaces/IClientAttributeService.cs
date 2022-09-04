using IoTPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface IClientAttributeService
    {
        Task<ClientAttribute> AddClientAttributeAsync(ClientAttribute clientAttribute);
        Task<ClientAttribute> FindClientAttributeByIdAsync(string id);
        Task<IEnumerable<ClientAttribute>> GetAllClientAttributesAsync();
        Task<ClientAttribute> UpdateClientAttributeAsync(string id, ClientAttribute clientAttribute);
        Task<bool> RemoveClientAttributeAsync(string id);
        Task<IEnumerable<ClientAttribute>> FindClientAttributeByDeviceIDAsync(string deviceID);
    }
}
