using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string ServiceDetailName { get; set; } = null!;

        public byte? Height { get; set; }

        public byte? Weight { get; set; }

        public string? BloodGroup { get; set; }

        public byte? BloodPressure { get; set; }

        public string? Allergieshistory { get; set; }

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
