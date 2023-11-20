using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class Service
{
    public int ServiceId { get; set; }

    public int ServiceTypeId { get; set; }

    public string ServiceName { get; set; } = null!;

    public virtual ICollection<ServiceSupply> ServiceSupplies { get; set; } = new List<ServiceSupply>();

    public virtual ServiceType ServiceType { get; set; } = null!;

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
