using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public DateTime CreateDate { get; set; }

    public string Diagnose { get; set; } = null!;

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<SuppliesPrescription> SuppliesPrescriptions { get; set; } = new List<SuppliesPrescription>();
}
