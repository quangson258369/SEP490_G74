using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class ExaminationResultIdMRDTO
    {
        public int ExamResultId { get; set; }

        public int MedicalRecordId { get; set; }

        public int DoctorId { get; set; }

        public string Diagnosis { get; set; } = null!;

        public string Conclusion { get; set; } = null!;

        public DateTime ExamDate { get; set; }

        public int ServiceId { get; set; }
    }
}
