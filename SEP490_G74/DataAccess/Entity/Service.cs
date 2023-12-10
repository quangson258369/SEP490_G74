using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class Service
{
    public int ServiceId { get; set; }

    public int ServiceTypeId { get; set; }

    public string ServiceName { get; set; } = null!;

    public decimal? Price { get; set; }
    [JsonIgnore]
    public virtual ICollection<ServiceMedicalRecord> ServiceMedicalRecords { get; set; } = new List<ServiceMedicalRecord>();
    [JsonIgnore]
    public virtual ServiceType ServiceType { get; set; } = null!;
}
