using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public DateTime CreateDate { get; set; }

    public string Diagnose { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    [JsonIgnore]
    public virtual ICollection<SuppliesPrescription> SuppliesPrescriptions { get; set; } = new List<SuppliesPrescription>();
}
