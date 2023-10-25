using System;
using System.Collections.Generic;

namespace API.Common.Entity;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int PatientId { get; set; }

    public int CashierId { get; set; }

    public DateTime PaymentDate { get; set; }

    public bool Status { get; set; }

    public decimal Total { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public virtual User Cashier { get; set; } = null!;

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Patient Patient { get; set; } = null!;
}
