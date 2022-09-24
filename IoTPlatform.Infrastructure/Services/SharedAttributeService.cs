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
    public class SharedAttributeService : ISharedAttributeService
    {
        private readonly ISharedAttributeRepository _sharedAttributeRepository;

        public SharedAttributeService(ISharedAttributeRepository sharedAttributeRepository)
        {
            _sharedAttributeRepository = sharedAttributeRepository;
        }

        public Task<SharedAttribute> AddSharedAttributeAsync(SharedAttribute sharedAttribute)
        {
            return _sharedAttributeRepository.Add(sharedAttribute);
        }

        public Task<IEnumerable<SharedAttribute>> FindSharedAttributeByDeviceIDAsync(string deviceID)
        {
            return _sharedAttributeRepository.FindSharedAttributeByDeviceID(deviceID);
        }

        public Task<SharedAttribute> FindSharedAttributeByIdAsync(string id)
        {
            return _sharedAttributeRepository.GetById(id);
        }

        public Task<IEnumerable<SharedAttribute>> GetAllSharedAttributesAsync()
        {
            return _sharedAttributeRepository.GetAll();
        }

        public Task<bool> RemoveSharedAttributeAsync(string id)
        {
            return _sharedAttributeRepository.Remove(id);
        }

        public Task<SharedAttribute> UpdateSharedAttributeAsync(string id, SharedAttribute sharedAttribute)
        {
            return _sharedAttributeRepository.Update(id, sharedAttribute);
        }
    }
}
