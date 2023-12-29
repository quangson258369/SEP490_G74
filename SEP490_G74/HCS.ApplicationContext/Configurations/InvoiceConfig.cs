using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.ApplicationContext.Configurations
{
    internal class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder
                .HasMany(c => c.InvoiceDetails)
                .WithOne(c => c.Invoice)
                .HasForeignKey(c => c.InvoiceId);

            builder
                .HasOne(c => c.Cashier)
                .WithMany(c => c.Invoices)
                .HasForeignKey(c => c.CashierId);
        }
    }
}
