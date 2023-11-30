using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class MedicalRecordModify
    {
        public int MedicalRecordId { get; set; }

        public int PatientId { get; set; }

        public DateTime MedicalRecordDate { get; set; }

        public string? ExamReason { get; set; }

        public string ExamCode { get; set; } = null!;

        public int DoctorId { get; set; }
    }
}
