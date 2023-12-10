using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class InvoiceDetail
{
    public int InvoiceDetailId { get; set; }

    public int InvoiceId { get; set; }

    public int MedicalRecordId { get; set; }
    [JsonIgnore]
    public virtual Invoice Invoice { get; set; } = null!;
    [JsonIgnore]
    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
