using IoTPlatform.Domain.Models.AuditLogs;
using IoTPlatform.Domain.Models.Devices;
using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel;
using System.Reflection;

namespace IoTPlatform.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;
        private const string inValidID = "Device ID provided is not a valid ID";

        public DeviceController(IDeviceService deviceService, IAuditLogService auditLogService, IUserService userService)
        {
            _deviceService = deviceService;
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
        public async Task<ActionResult> AddDevice(Device device)
        {
            var result = await _deviceService.AddDeviceAsync(device);

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();

            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                result.DeviceName, userInfor[0], userInfor[1], ActionType.Create, status);

            return new JsonResult(new { result });
         
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDevices()
        {
            var result = await _deviceService.GetAllDevicesAsync();
            if (!result.Any())
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindDeviceByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _deviceService.FindDeviceByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindDeviceDetailByID(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _deviceService.FindDeviceDetailByIDAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDevice(string id, Device device)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _deviceService.UpdateDeviceAsync(id, device);
            if (result == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                result.DeviceName, userInfor[0], userInfor[1], ActionType.Update, status);

            return new JsonResult(new { result });

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveDevice(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var obj = await _deviceService.FindDeviceByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, obj.DeviceID,
                obj.DeviceName, userInfor[0], userInfor[1], ActionType.Delete, status);

            var result = await _deviceService.RemoveDeviceAsync(id);
            return new JsonResult(new { result });
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> FindDeviceByName(string name)
        {
            var result = await _deviceService.FindDeviceByNameAsync(name);
            if (!result.Any())
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{deviceProfileID}")]
        public async Task<ActionResult> FindDeviceByDeviceProfileID(string deviceProfileID)
        {
            var isValid = ObjectId.TryParse(deviceProfileID, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _deviceService.FindDeviceByDeviceProfileIDAsync(deviceProfileID);
            if (!result.Any())
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpGet("{customerID}")]
        public async Task<ActionResult> FindDeviceByCustomerID(string customerID)
        {
            var isValid = ObjectId.TryParse(customerID, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _deviceService.FindDeviceByCustomerIDAsync(customerID);
            if (!result.Any())
            {
                return NotFound();
            }
            return new JsonResult(new { result });
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> MakeDevicePublic(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _deviceService.MakeDevicePublicAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                result.DeviceName, userInfor[0], userInfor[1], ActionType.MakePublic, status);
            return new JsonResult(new { result });
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> MakeDevicePrivate(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _deviceService.MakeDevicePrivateAysnc(id);
            if (result == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                result.DeviceName, userInfor[0], userInfor[1], ActionType.MakePrivate, status);

            return new JsonResult(new { result });
        }

        [HttpPost("{id}&&{customerID}")]
        public async Task<ActionResult> AssignDeviceToCustomer(string id, string customerID)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);
            }

            var result = await _deviceService.AssignDeviceToCustomerAsync(id, customerID);
            if (result == null)
            {
                return NotFound();
            }
            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                result.DeviceName, userInfor[0], userInfor[1], ActionType.AssignCustomer, status);
            return new JsonResult(new { result });
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UnAssignDeviceToCustomer(string id)
        {
            var isValid = ObjectId.TryParse(id, out _);
            if (!isValid)
            {
                return BadRequest(inValidID);

            }
            var result = await _deviceService.UnAssignDeviceToCustomerAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            var userInfor = _userService.GetUserInformation();
            var status = GetResponseStatus();
            await _auditLogService.AddAnAuditLogAsync(DateTime.Now, EntityType.Device, result.DeviceID,
                result.DeviceName, userInfor[0], userInfor[1], ActionType.UnAssignCustomer, status);

            return new JsonResult(new { result });
        }
    }
}
