using System;
using System.Collections.Generic;

namespace API.Common.Entity;

public partial class InvoiceDetail
{
    public int InvoiceDetailId { get; set; }

    public int InvoiceId { get; set; }

    public int MedicalRecordId { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
