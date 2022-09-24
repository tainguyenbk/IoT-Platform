using IoTPlatform.Domain.Models.Attribute;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using IoTPlatform.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services
{
    public class ClientAttributeService : IClientAttributeService
    {
        private readonly IClientAttributeRepository _clientAttributeRepository;

        public ClientAttributeService(IClientAttributeRepository clientAttributeRepository)
        {
            _clientAttributeRepository = clientAttributeRepository;
        }

        public Task<ClientAttribute> AddClientAttributeAsync(ClientAttribute clientAttribute)
        {
            return _clientAttributeRepository.Add(clientAttribute);
        }

        public Task<IEnumerable<ClientAttribute>> FindClientAttributeByDeviceIDAsync(string deviceID)
        {
            return _clientAttributeRepository.FindClientAttributeByDeviceID(deviceID);
        }

        public Task<ClientAttribute> FindClientAttributeByIdAsync(string id)
        {
            return _clientAttributeRepository.GetById(id);
        }

        public Task<IEnumerable<ClientAttribute>> GetAllClientAttributesAsync()
        {
            return _clientAttributeRepository.GetAll();
        }

        public Task<bool> RemoveClientAttributeAsync(string id)
        {
            return _clientAttributeRepository.Remove(id);
        }

        public Task<ClientAttribute> UpdateClientAttributeAsync(string id, ClientAttribute clientAttribute)
        {
            return _clientAttributeRepository.Update(id, clientAttribute);
        }
    }
}
