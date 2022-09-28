using IoTPlatform.Domain.Models.Customers;
using IoTPlatform.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<IEnumerable<Customer>> FindCustomerByTitle(string title);
    }
}
