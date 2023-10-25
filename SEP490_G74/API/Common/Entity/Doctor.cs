using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("Doctor")]
public partial class Doctor
{
    [Key]
    [Column("userId")]
    public int UserId { get; set; }

    [Column("doctorSpecialist")]
    [StringLength(150)]
    public string DoctorSpecialist { get; set; } = null!;

    [Column("serviceTypeId")]
    public int ServiceTypeId { get; set; }

    [InverseProperty("UserNavigation")]
    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    [InverseProperty("Doctor")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    [ForeignKey("ServiceTypeId")]
    [InverseProperty("Doctors")]
    public virtual ServiceType ServiceType { get; set; } = null!;
}
