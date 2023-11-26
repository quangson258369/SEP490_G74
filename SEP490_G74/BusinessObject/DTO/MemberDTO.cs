using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        public string? Name { get; set; }
        public string? Gmail { get; set; }
        public string? Phone { get; set; }
        public string? RoleName { get; set; }
        public string? Address { get; set; }
    }
}
