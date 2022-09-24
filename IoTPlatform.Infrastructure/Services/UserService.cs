using IoTPlatform.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string[] GetUserInformation()
        {
            var userInfor = new string[2];
            if (_httpContextAccessor.HttpContext != null)
            {
                var name = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type == "UserName");
                var id = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type == "UserID");

                if (name != null && id != null)
                {
                    userInfor[0] = name.Value;
                    userInfor[1] = id.Value;
                }
            }
            return userInfor;
        }
    }
}
