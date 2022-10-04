using IoTPlatform.Domain.Models.Customers;
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
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IDeviceRepository _deviceRepository;

        public CustomerService(ICustomerRepository customerRepository, IAuditLogRepository auditLogRepository, IDeviceRepository deviceRepository)
        {
            _customerRepository = customerRepository;
            _auditLogRepository = auditLogRepository;
            _deviceRepository = deviceRepository;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            return await _customerRepository.Add(customer);
        }

        public async Task<Customer> FindCustomerByIdAsync(string id)
        {
            return await _customerRepository.GetById(id);
        }

        public async Task<IEnumerable<Customer>> FindCustomerByTitleAsync(string title)
        {
            return await _customerRepository.FindCustomerByTitle(title);
        }

        public async Task<CustomerResponse> FindCustomerDetailByIdAsync(string id)
        {
            var customer = await _customerRepository.GetById(id);

            if (customer != null)
            {
                var auditLogs = await _auditLogRepository.FindAuditLogsByEntityID(customer.CustomerID);
                var devices = await _deviceRepository.FindDeviceByCustomerID(customer.CustomerID);

                CustomerResponse deviceResponse = new CustomerResponse()
                {
                    Customer = customer,
                    Devices = devices.ToList(),
                    AuditLogs = auditLogs.ToList()
                };
                return deviceResponse;
            }
            return null;
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync()
        { 
            var result = new List<CustomerResponse>();
            var listCustomer = await _customerRepository.GetAll();

            foreach (var customer in listCustomer)
            {
                var auditLogs = await _auditLogRepository.FindAuditLogsByEntityID(customer.CustomerID);
                var devices = await _deviceRepository.FindDeviceByCustomerID(customer.CustomerID);

                CustomerResponse customerResponse = new CustomerResponse()
                {
                    Customer = customer,
                    Devices = devices.ToList(),
                    AuditLogs = auditLogs.ToList()
                };
                result.Add(customerResponse);
            }
            return result;
        }

        public async Task<bool> RemoveCustomerAsync(string id)
        {
            return await _customerRepository.Remove(id);
        }

        public async Task<Customer> UpdateCustomerAsync(string id, Customer customer)
        {
            return await _customerRepository.Update(id, customer);
        }
    }
}
