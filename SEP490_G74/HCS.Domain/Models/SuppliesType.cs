
namespace HCS.Domain.Models
{
    public class SuppliesType
    {
        public int SuppliesTypeId { get; set; }

        public string SuppliesTypeName { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
    }
}
