using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public DateTime CreateDate { get; set; }

    public string Diagnose { get; set; } = null!;

    public byte Quantity { get; set; }

    public int MedicalRecordId { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual ICollection<Supply> SIds { get; set; } = new List<Supply>();
}
