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
    internal class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            builder
                .HasMany(c => c.MedicalRecords)
                .WithOne(c => c.Patient)
                .HasForeignKey(c => c.PatientId);

            builder
                .HasOne(c => c.Contact)
                .WithOne(c => c.Patient)
                .HasForeignKey<Patient>(c => c.ContactId);

            builder
                .HasMany(c => c.Invoices)
                .WithOne(c => c.Patient)
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasData(
                    new Patient()
                    {
                        PatientId = 1,
                        Allergieshistory = "None",
                        BloodGroup = "A",
                        BloodPressure = 128,
                        ContactId = 10,
                        Height = 157,
                        ServiceDetailName = "None",
                        Weight = 50
                    },
                    new Patient()
                    {
                        PatientId = 2,
                        Allergieshistory = "None",
                        BloodGroup = "A",
                        BloodPressure = 128,
                        ContactId = 11,
                        Height = 157,
                        ServiceDetailName = "None",
                        Weight = 50
                    },
                    new Patient()
                    {
                        PatientId = 3,
                        Allergieshistory = "None",
                        BloodGroup = "A",
                        BloodPressure = 128,
                        ContactId = 12,
                        Height = 157,
                        ServiceDetailName = "None",
                        Weight = 50
                    }
                );
        }
    }
}
