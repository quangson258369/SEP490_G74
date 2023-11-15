using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("MedicalRecord")]
public partial class MedicalRecord
{
    [Key]
    [Column("medicalRecordID")]
    public int MedicalRecordId { get; set; }

    [Column("patientId")]
    public int PatientId { get; set; }

    [Column("medicalRecordDate", TypeName = "datetime")]
    public DateTime MedicalRecordDate { get; set; }

    [Column("examReason")]
    [StringLength(250)]
    public string? ExamReason { get; set; }

    [Column("examCode")]
    [StringLength(50)]
    [Unicode(false)]
    public string ExamCode { get; set; } = null!;

    [Column("doctorId")]
    public int DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("MedicalRecords")]
    public virtual Doctor? Doctor { get; set; }

    [InverseProperty("MedicalRecord")]
    public virtual ICollection<ExaminationResultId>? ExaminationResultIds { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("MedicalRecords")]
    public virtual Patient? Patient { get; set; }

    [InverseProperty("MedicalRecord")]
    public virtual ICollection<Prescription>? Prescriptions { get; set; }

    [ForeignKey("MedicalRecordId")]
    [InverseProperty("MedicalRecords")]
    public virtual ICollection<Service>? Services { get; set; }
}
