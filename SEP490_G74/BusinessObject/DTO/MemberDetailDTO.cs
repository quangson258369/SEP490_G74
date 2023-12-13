using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class MemberDetailDTO
    {
        public int MemberId { get; set; }
        public string? Name { get; set; }
        public bool? Gender { get; set; }
        public string? Gmail { get; set; }
        public string? Phone { get; set; }
        public string? RoleName { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }
        public bool Status { get; set; }
        public string? ImageLink { get; set; }
        public int UserId { get; set; }
        public string? ServiceType { get; set; }

    }
}
