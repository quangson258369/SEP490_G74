
namespace HCS.Domain.Models
{
    public class SuppliesPrescription
    {
        public int SId { get; set; }

        public int Quantity { get; set; }

        public int PrescriptionId { get; set; }

        public Prescription Prescription { get; set; } = null!;

        public int SupplyId { get; set; }

        public Supply Supply { get; set; } = null!;
    }
}
