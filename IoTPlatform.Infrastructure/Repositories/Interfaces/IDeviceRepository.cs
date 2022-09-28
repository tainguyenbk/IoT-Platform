using IoTPlatform.Domain.Models.Devices;
using IoTPlatform.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories.Interfaces
{
    public interface IDeviceRepository : IBaseRepository<Device>
    {
        Task<IEnumerable<Device>> FindDeviceByName(string name);
        Task<IEnumerable<Device>> FindDeviceByDeviceProfile(string deviceProfile);
        Task<IEnumerable<Device>> FindDeviceByLabel(string label);
        Task<IEnumerable<Device>> FindDeviceByCustomer(string customer);
    }
}
