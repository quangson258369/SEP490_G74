using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class Service
{
    public int ServiceId { get; set; }

    public int ServiceTypeId { get; set; }

    public string ServiceName { get; set; } = null!;

    public decimal? Price { get; set; }

    public virtual ICollection<ServiceMedicalRecord> ServiceMedicalRecords { get; set; } = new List<ServiceMedicalRecord>();

    public virtual ICollection<ServiceSupply> ServiceSupplies { get; set; } = new List<ServiceSupply>();

    public virtual ServiceType ServiceType { get; set; } = null!;
}
