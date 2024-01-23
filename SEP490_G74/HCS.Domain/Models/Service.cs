
namespace HCS.Domain.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        
        public string ServiceName { get; set; } = null!;

        public double Price { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<ServiceMedicalRecord>? ServiceMedicalRecords { get; set; }

        public int ServiceTypeId { get; set; }

        public ServiceType ServiceType { get; set; } = null!;
    }
}
