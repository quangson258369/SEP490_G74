using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public DateTime CreateDate { get; set; }

    public string Diagnose { get; set; } = null!;

    public byte Quantity { get; set; }

    public int MedicalRecordId { get; set; }
    [JsonIgnore]
    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Supply> SIds { get; set; } = new List<Supply>();
}
