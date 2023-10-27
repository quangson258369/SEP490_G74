using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("ExaminationResultId")]
public partial class ExaminationResultId
{
    [Key]
    [Column("examResultId")]
    public int ExamResultId { get; set; }

    [Column("medicalRecordID")]
    public int MedicalRecordId { get; set; }

    [Column("doctorId")]
    public int DoctorId { get; set; }

    [Column("diagnosis")]
    [StringLength(350)]
    public string Diagnosis { get; set; } = null!;

    [Column("conclusion")]
    [StringLength(350)]
    public string Conclusion { get; set; } = null!;

    [Column("examDate", TypeName = "datetime")]
    public DateTime ExamDate { get; set; }

    [Column("serviceId")]
    public int ServiceId { get; set; }

    [ForeignKey("MedicalRecordId")]
    [InverseProperty("ExaminationResultIds")]
    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
