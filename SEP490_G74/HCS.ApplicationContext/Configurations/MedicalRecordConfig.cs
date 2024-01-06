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
                .HasOne(c => c.ExaminationResult)
                .WithOne(c => c.MedicalRecord)
                .HasForeignKey<MedicalRecord>(c => c.ExaminationResultId);

            builder
                .HasData(
                    new MedicalRecord() { MedicalRecordId = 1, ExamReason = "patient 1 mr1", PatientId = 1 },
                    new MedicalRecord() { MedicalRecordId = 2, ExamReason = "patient 2 mr2", PatientId = 2 }
                );
        }
    }
}
