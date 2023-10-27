using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("Invoice")]
public partial class Invoice
{
    [Key]
    [Column("invoiceId")]
    public int InvoiceId { get; set; }

    [Column("patientId")]
    public int PatientId { get; set; }

    [Column("cashierId")]
    public int CashierId { get; set; }

    [Column("paymentDate", TypeName = "datetime")]
    public DateTime PaymentDate { get; set; }

    [Column("status")]
    public bool Status { get; set; }

    [Column("total", TypeName = "money")]
    public decimal Total { get; set; }

    [Column("paymentMethod")]
    [StringLength(50)]
    public string PaymentMethod { get; set; } = null!;

    [ForeignKey("CashierId")]
    [InverseProperty("Invoices")]
    public virtual User Cashier { get; set; } = null!;

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [ForeignKey("PatientId")]
    [InverseProperty("Invoices")]
    public virtual Patient Patient { get; set; } = null!;
}
