using IoTPlatform.Domain.Models;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(Customer customer)
        {
            var result = await _customerService.AddCustomerAsync(customer);
            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAllCustomersAsync();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindCustomerByID(string id)
        {
            var result = await _customerService.FindCustomerByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(string id, Customer customer)
        {
            var result = await _customerService.UpdateCustomerAsync(id, customer);
            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCustomer(string id)
        {
            var result = await _customerService.RemoveCustomerAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{title}")]
        public async Task<ActionResult> FindCustomerByName(string title)
        {
            var result = await _customerService.FindCustomerByTitleAsync(title);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }
    }
}
