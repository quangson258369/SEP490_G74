using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class SuppliesType
{
    public int SuppliesTypeId { get; set; }

    public string SuppliesTypeName { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
