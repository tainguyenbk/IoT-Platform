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
    public class ServerAttributeService : IServerAttributeService
    {
        private readonly IServerAttributeRepository _serverAttributeRepository;

        public ServerAttributeService(IServerAttributeRepository serverAttributeRepository)
        {
            _serverAttributeRepository = serverAttributeRepository;
        }

        public Task<ServerAttribute> AddServerAttributeAsync(ServerAttribute serverAttribute)
        {
            return _serverAttributeRepository.Add(serverAttribute);
        }

        public Task<IEnumerable<ServerAttribute>> FindServerAttributeByDeviceIDAsync(string deviceID)
        {
            return _serverAttributeRepository.FindServerAttributeByDeviceID(deviceID);
        }

        public Task<ServerAttribute> FindServerAttributeByIdAsync(string id)
        {
            return _serverAttributeRepository.GetById(id);
        }

        public Task<IEnumerable<ServerAttribute>> GetAllServerAttributesAsync()
        {
            return _serverAttributeRepository.GetAll();
        }

        public Task<bool> RemoveServerAttributeAsync(string id)
        {
            return _serverAttributeRepository.Remove(id);
        }

        public Task<ServerAttribute> UpdateServerAttributeAsync(string id, ServerAttribute serverAttribute)
        {
            return _serverAttributeRepository.Update(id, serverAttribute);
        }
    }
}
