using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class SuppliesPrescription
{
    public int SId { get; set; }

    public int PrescriptionId { get; set; }

    public int? Quantity { get; set; }
    [JsonIgnore]
    public virtual Prescription Prescription { get; set; } = null!;
    [JsonIgnore]
    public virtual Supply SIdNavigation { get; set; } = null!;
}
