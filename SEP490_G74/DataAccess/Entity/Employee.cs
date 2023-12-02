using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class Employee
{
    public int DoctorId { get; set; }

    public int? ServiceTypeId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<ServiceMedicalRecord> ServiceMedicalRecords { get; set; } = new List<ServiceMedicalRecord>();

    public virtual ServiceType? ServiceType { get; set; }

    public virtual User User { get; set; } = null!;
}
