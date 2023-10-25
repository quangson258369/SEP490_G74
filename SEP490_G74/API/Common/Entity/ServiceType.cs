using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("ServiceType")]
public partial class ServiceType
{
    [Key]
    [Column("serviceTypeId")]
    public int ServiceTypeId { get; set; }

    [Column("serviceTypeName")]
    [StringLength(150)]
    public string ServiceTypeName { get; set; } = null!;

    [InverseProperty("ServiceType")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    [InverseProperty("ServiceType")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
