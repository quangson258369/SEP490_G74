using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("Service")]
public partial class Service
{
    [Key]
    [Column("serviceId")]
    public int ServiceId { get; set; }

    [Column("serviceTypeId")]
    public int ServiceTypeId { get; set; }

    [Column("serviceName")]
    [StringLength(150)]
    public string ServiceName { get; set; } = null!;


    [InverseProperty("Service")]
    public virtual ICollection<ServiceSupply> ServiceSupplies { get; set; } = new List<ServiceSupply>();

    [ForeignKey("ServiceTypeId")]
    [InverseProperty("Services")]
    public virtual ServiceType ServiceType { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("Services")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
