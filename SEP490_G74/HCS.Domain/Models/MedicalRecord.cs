using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Models
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }

        public DateTime MedicalRecordDate { get; set; } = DateTime.Now;

        public string ExamReason { get; set; } = string.Empty;

        public string ExamCode { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public int PatientId { get; set; }

        public Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }

        public User Doctor { get; set; } = null!;

        public int PrescriptionId { get; set; }

        public Prescription Prescription { get; set; } = null!;

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

        public ICollection<ExaminationResult> ExaminationResults { get; set; } = new List<ExaminationResult>();

        public ICollection<ServiceMedicalRecord> ServiceMedicalRecords { get; set; } = new List<ServiceMedicalRecord>();
    }
}
