using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class SuppliesPrescription
{
    public int SId { get; set; }

    public int PrescriptionId { get; set; }

    public int? Quantity { get; set; }

    public virtual Prescription Prescription { get; set; } = null!;

    public virtual Supply SIdNavigation { get; set; } = null!;
}
