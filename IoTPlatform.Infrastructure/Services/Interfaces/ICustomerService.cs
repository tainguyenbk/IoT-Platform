using IoTPlatform.Domain.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services.Interfaces
{
    public interface ICustomerService 
    {
        Task<Customer> AddCustomerAsync(Customer Customer);
        Task<Customer> FindCustomerByIdAsync(string id);
        Task<CustomerResponse> FindCustomerDetailByIdAsync(string id);
        Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync();
        Task<Customer> UpdateCustomerAsync(string id, Customer Customer);
        Task<bool> RemoveCustomerAsync(string id);
        Task<IEnumerable<Customer>> FindCustomerByTitleAsync(string title);
    }
}
