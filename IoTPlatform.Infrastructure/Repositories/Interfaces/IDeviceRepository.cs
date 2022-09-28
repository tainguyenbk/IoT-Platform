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
        Task<IEnumerable<Device>> FindDeviceByDeviceProfileID(string deviceProfileID);
        Task<IEnumerable<Device>> FindDeviceByCustomerID(string customerID);
    }
}
