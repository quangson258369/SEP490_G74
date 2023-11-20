using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorSpecialize { get; set; } = null!;

    public int ServiceTypeId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ServiceType ServiceType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
