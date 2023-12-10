using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class Patient
{
    public int PatientId { get; set; }

    public string ServiceDetailName { get; set; } = null!;

    public byte? Height { get; set; }

    public byte? Weight { get; set; }

    public string? BloodGroup { get; set; }

    public byte? BloodPressure { get; set; }

    public string? Allergieshistory { get; set; }
    [JsonIgnore]
    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    [JsonIgnore]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    [JsonIgnore]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
