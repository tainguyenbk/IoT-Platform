using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Domain.Models.Customers;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IoTPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;
        private const string inValidID = "Customer ID provided is not a valid ID";

        public CustomerController(ICustomerService customerService, IUserService userService, IAuditLogService auditLogService)
        {
            _customerService = customerService;
            _userService = userService;
            _auditLogService = auditLogService;
        }

        private string GetResponseStatus()
        {
            string status;
            if (Response.StatusCode == 200)
            {
                status = "Success";
            }
            else
            {
                status = "Failure";
            }
            return status;
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(Customer customer)
        {
            var result = await _customerService.AddCustomerAsync(customer);

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Custormer, result.CustomerID, result.Title, userInfor[0], userInfor[1], ActionType.Create, status);

            return new JsonResult(new { result });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            var listCustomer = await _customerService.GetAllCustomersAsync();
            if (!listCustomer.Any())
            {
                return NotFound();
            }

            var result = listCustomer.OrderByDescending(o => o.Customer.CreatedTime);
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindCustomerByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _customerService.FindCustomerByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindCustomerDetailByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _customerService.FindCustomerDetailByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(string id, Customer customer)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _customerService.UpdateCustomerAsync(id, customer);
            if(result == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Custormer, result.CustomerID, result.Title, userInfor[0], userInfor[1], ActionType.Update, status);

            return new JsonResult(new { result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCustomer(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var obj = await _customerService.FindCustomerByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Custormer, obj.CustomerID, obj.Title, userInfor[0], userInfor[1], ActionType.Delete, status);
            
            var result = await _customerService.RemoveCustomerAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{title}")]
        public async Task<ActionResult> FindCustomerByName(string title)
        {
            var listCustomer = await _customerService.FindCustomerByTitleAsync(title);
            if (!listCustomer.Any())
            {
                return NotFound();
            }

            var result = listCustomer.OrderByDescending(o => o.CreatedTime);
            return new JsonResult(new { result });
        }
    }
}
