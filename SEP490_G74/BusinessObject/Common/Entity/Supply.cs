using System;
using System.Collections.Generic;

namespace API.Common.Entity;

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

    public virtual ICollection<ServiceSupply> ServiceSupplies { get; set; } = new List<ServiceSupply>();

    public virtual SuppliesType SuppliesType { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
