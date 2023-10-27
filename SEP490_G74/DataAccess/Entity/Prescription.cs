using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("Prescription")]
public partial class Prescription
{
    [Key]
    public int PrescriptionId { get; set; }

    [Column("createDate", TypeName = "date")]
    public DateTime CreateDate { get; set; }

    [Column("diagnose")]
    [StringLength(550)]
    public string Diagnose { get; set; } = null!;

    [Column("quantity")]
    public byte Quantity { get; set; }

    [Column("medicalRecordId")]
    public int MedicalRecordId { get; set; }

    [ForeignKey("MedicalRecordId")]
    [InverseProperty("Prescriptions")]
    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    [ForeignKey("PrescriptionId")]
    [InverseProperty("Prescriptions")]
    public virtual ICollection<Supply> SIds { get; set; } = new List<Supply>();
}
