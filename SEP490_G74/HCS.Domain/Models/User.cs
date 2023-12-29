using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool? Status { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;

        public int? CategoryId { get; set; } = null;

        public Category? Category { get; set; } = null;

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
