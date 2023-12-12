using API.Common;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class MedicalRecordOutputDto : BaseOutputCommon
    {
        
        public string PatientName { get; set; } = null!;

        public string PatientPhone { get; set; } = null!;

        public int MedicalRecordId { get; set; }

        public int PatientId { get; set; }

        public DateTime MedicalRecordDate { get; set; }

        public string? ExamReason { get; set; }

        public string ExamCode { get; set; } = null!;

        public int DoctorId { get; set; }

        public int? PrescriptionId { get; set; }

        public Employee? Doctor { get; set; }

        public Patient? Patient { get; set; }

        public List<ExaminationResultId>? ExaminationResultIds { get; set; }

        public Prescription? Prescriptions { get; set; }

        public List<Service>? Services { get; set; }

        public List<InvoiceDetail>? InvoiceDetails { get; set; }

        public List<ServiceMedicalRecord>? ServiceMedicalRecords { get; set; } 
    }
}
