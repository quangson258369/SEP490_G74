using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class ServiceMedicalRecord
{
    public int ServiceId { get; set; }

    public int MedicalRecordId { get; set; }

    public int? DoctorId { get; set; }

    public virtual Employee? Doctor { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
