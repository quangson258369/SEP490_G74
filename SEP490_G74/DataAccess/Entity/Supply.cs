using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class Supply
{
    public int SId { get; set; }

    public string SName { get; set; } = null!;

    public string Uses { get; set; } = null!;

    public DateTime Exp { get; set; }

    public string Distributor { get; set; } = null!;

    public short UnitInStock { get; set; }

    public decimal Price { get; set; }

    public int SuppliesTypeId { get; set; }

    public DateTime? Inputday { get; set; }
    [JsonIgnore]
    public virtual ICollection<ServiceSupply> ServiceSupplies { get; set; } = new List<ServiceSupply>();
    [JsonIgnore]
    public virtual SuppliesType SuppliesType { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
