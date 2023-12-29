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
    internal class InvoiceDetailConfig : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder
                .HasOne(c => c.MedicalRecord)
                .WithMany(c => c.InvoiceDetails)
                .HasForeignKey(c => c.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder
                .HasOne(c => c.Invoice)
                .WithMany(c => c.InvoiceDetails)
                .HasForeignKey(c => c.InvoiceId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
