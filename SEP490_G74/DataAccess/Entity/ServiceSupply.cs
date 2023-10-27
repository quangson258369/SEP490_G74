using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[PrimaryKey("Sid", "ServiceId")]
public partial class ServiceSupply
{
    [Key]
    [Column("sid")]
    public int Sid { get; set; }

    [Key]
    [Column("serviceId")]
    public int ServiceId { get; set; }

    [Column("quantity")]
    public byte Quantity { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("ServiceSupplies")]
    public virtual Service Service { get; set; } = null!;

    [ForeignKey("Sid")]
    [InverseProperty("ServiceSupplies")]
    public virtual Supply SidNavigation { get; set; } = null!;
}
