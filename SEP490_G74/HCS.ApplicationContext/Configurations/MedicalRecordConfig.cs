using HCS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.ApplicationContext.Configurations
{
    internal class MedicalRecordConfig : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder
                .HasMany(c => c.InvoiceDetails)
                .WithOne(c => c.MedicalRecord)
                .HasForeignKey(c => c.MedicalRecordId);

            builder
                .HasMany(c => c.ExaminationResults)
                .WithOne(c => c.MedicalRecord)
                .HasForeignKey(c => c.MedicalRecordId);

            builder
                .HasOne(c => c.Category)
                .WithMany(c => c.MedicalRecords)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}
