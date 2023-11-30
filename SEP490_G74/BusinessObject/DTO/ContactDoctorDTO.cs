using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class ContactDoctorDTO
    {
        public int CId { get; set; }

        public string Name { get; set; } = null!;

        public bool Gender { get; set; }

        public string Phone { get; set; } = null!;

        public DateTime Dob { get; set; }

        public string Address { get; set; } = null!;

        public string? Img { get; set; }

        public int? DoctorId { get; set; }
    }
}
