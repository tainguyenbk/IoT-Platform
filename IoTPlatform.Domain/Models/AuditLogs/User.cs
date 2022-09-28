using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlatform.Domain.Models.AuditLogs
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
