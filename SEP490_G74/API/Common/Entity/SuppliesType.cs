using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("SuppliesType")]
public partial class SuppliesType
{
    [Key]
    [Column("suppliesTypeId")]
    public int SuppliesTypeId { get; set; }

    [Column("suppliesTypeName")]
    [StringLength(150)]
    public string SuppliesTypeName { get; set; } = null!;

    [InverseProperty("SuppliesType")]
    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
