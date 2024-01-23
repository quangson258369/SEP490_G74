

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

        public int? ContactId { get; set; }

        public Contact? Contact { get; set; } = null!;

        public virtual ICollection<Invoice>? Invoices { get; set; }

        public ICollection<MedicalRecord>? MedicalRecords { get; set; }
    }
}
