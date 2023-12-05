using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class ServiceMedicalRecord
{
    public int ServiceId { get; set; }

    public int MedicalRecordId { get; set; }

    public int? DoctorId { get; set; }
    [JsonIgnore]
    public virtual Employee? Doctor { get; set; }
    [JsonIgnore]
    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
    [JsonIgnore]
    public virtual Service Service { get; set; } = null!;
}
