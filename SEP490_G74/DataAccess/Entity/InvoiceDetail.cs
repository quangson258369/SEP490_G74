using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Entity;

[Table("InvoiceDetail")]
public partial class InvoiceDetail
{
    [Key]
    [Column("invoiceDetailId")]
    public int InvoiceDetailId { get; set; }

    [Column("invoiceId")]
    public int InvoiceId { get; set; }

    [Column("medicalRecordId")]
    public int MedicalRecordId { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Invoice Invoice { get; set; } = null!;
}
