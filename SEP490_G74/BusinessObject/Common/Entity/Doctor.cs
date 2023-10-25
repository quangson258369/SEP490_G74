using System;
using System.Collections.Generic;

namespace API.Common.Entity;

public partial class Doctor
{
    public int UserId { get; set; }

    public string DoctorSpecialist { get; set; } = null!;

    public int ServiceTypeId { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ServiceType ServiceType { get; set; } = null!;
}
