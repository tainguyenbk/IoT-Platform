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
        private readonly IHttpContextAccessor _httpContextAccesstor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccesstor = httpContextAccessor;
        }

        public string[] GetUserInformation()
        {
            var userInfor = new string[2];
            
            if (_httpContextAccesstor.HttpContext != null)
            {
                var userName = _httpContextAccesstor.HttpContext.User.FindFirst(claim => claim.Type == "UserName");
                var userId = _httpContextAccesstor.HttpContext.User.FindFirst(claim => claim.Type == "UserId");

                if (userName != null && userId != null)
                {
                    userInfor[0] = userName.Value;
                    userInfor[1] = userId.Value;
                }
            }
            return userInfor;
        }
    }
}
