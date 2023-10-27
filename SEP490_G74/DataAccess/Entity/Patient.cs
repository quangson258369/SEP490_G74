using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("Patient")]
public partial class Patient
{
    [Key]
    [Column("patientId")]
    public int PatientId { get; set; }

    [Column("serviceDetailName")]
    [StringLength(350)]
    public string ServiceDetailName { get; set; } = null!;

    [Column("examDate", TypeName = "datetime")]
    public DateTime ExamDate { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    [InverseProperty("Patient")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Patient")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
