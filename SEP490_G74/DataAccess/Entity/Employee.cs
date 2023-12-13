using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class Employee
{
    public int DoctorId { get; set; }

    public int? ServiceTypeId { get; set; }

    public int UserId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    [JsonIgnore]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    [JsonIgnore]
    public virtual ICollection<ServiceMedicalRecord> ServiceMedicalRecords { get; set; } = new List<ServiceMedicalRecord>();
    [JsonIgnore]
    public virtual ServiceType? ServiceType { get; set; }
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
