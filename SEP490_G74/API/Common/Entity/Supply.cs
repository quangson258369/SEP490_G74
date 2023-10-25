using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

public partial class Supply
{
    [Key]
    [Column("sId")]
    public int SId { get; set; }

    [Column("sName")]
    [StringLength(150)]
    public string SName { get; set; } = null!;

    [Column("uses")]
    [StringLength(500)]
    public string Uses { get; set; } = null!;

    [Column("exp", TypeName = "date")]
    public DateTime Exp { get; set; }

    [Column("distributor")]
    [StringLength(150)]
    public string Distributor { get; set; } = null!;

    [Column("unitInStock")]
    public short UnitInStock { get; set; }

    [Column("price", TypeName = "money")]
    public decimal Price { get; set; }

    [Column("suppliesTypeId")]
    public int SuppliesTypeId { get; set; }

    [InverseProperty("SidNavigation")]
    public virtual ICollection<ServiceSupply> ServiceSupplies { get; set; } = new List<ServiceSupply>();

    [ForeignKey("SuppliesTypeId")]
    [InverseProperty("Supplies")]
    public virtual SuppliesType SuppliesType { get; set; } = null!;

    [ForeignKey("SId")]
    [InverseProperty("SIds")]
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
