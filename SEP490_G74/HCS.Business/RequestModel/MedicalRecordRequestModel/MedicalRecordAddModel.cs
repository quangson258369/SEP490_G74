using HCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Business.RequestModel.MedicalRecordRequestModel
{
    public class MedicalRecordAddModel
    {
        public string ExamReason { get; set; } = string.Empty;

        public string ExamCode { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }
    }
}
