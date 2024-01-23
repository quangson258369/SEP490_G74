

namespace HCS.Domain.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string Diagnose { get; set; } = string.Empty;

        public ExaminationResult ExaminationResult { get; set; } = null!;

        public ICollection<SuppliesPrescription>? SuppliesPrescriptions { get; set; }
    }
}
