using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain
{
    public class UserJWTModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = null!;

        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}
