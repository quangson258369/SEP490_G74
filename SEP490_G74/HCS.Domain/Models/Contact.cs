using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Models
{
    public class Contact
    {
        public int CId { get; set; }

        public string Name { get; set; } = null!;

        public bool Gender { get; set; }

        public string Phone { get; set; } = string.Empty;

        public DateTime Dob { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Img { get; set; } = string.Empty;

        public int? PatientId { get; set; }

        public Patient? Patient { get; set; }

        public int? UserId { get; set; } 

        public User? User { get; set; }
    }
}
