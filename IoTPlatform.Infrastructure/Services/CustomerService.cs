using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Repositories.Interfaces;
using IoTPlatform.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<Customer> AddCustomerAsync(Customer customer)
        {
            return _customerRepository.Add(customer);
        }

        public Task<Customer> FindCustomerByIdAsync(string id)
        {
            return _customerRepository.GetById(id);
        }

        public Task<IEnumerable<Customer>> FindCustomerByTitleAsync(string title)
        {
            return _customerRepository.FindCustomerByTitle(title);
        }

        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return _customerRepository.GetAll();
        }

        public Task<bool> RemoveCustomerAsync(string id)
        {
            return _customerRepository.Remove(id);
        }

        public Task<Customer> UpdateCustomerAsync(string id, Customer customer)
        {
            return _customerRepository.Update(id, customer);
        }
    }
}
