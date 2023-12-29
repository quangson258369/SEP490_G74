using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        
        public string ServiceName { get; set; } = null!;

        public decimal Price { get; set; }

        public ICollection<ServiceMedicalRecord> ServiceMedicalRecords { get; set; } = new List<ServiceMedicalRecord>();

        public int ServiceTypeId { get; set; }

        public ServiceType ServiceType { get; set; } = null!;
    }
}
