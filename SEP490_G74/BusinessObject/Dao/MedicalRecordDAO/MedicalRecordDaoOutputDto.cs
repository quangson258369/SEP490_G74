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

namespace HcsBE.Dao.MedicalRecordDAO
{
    public class MedicalRecordDaoOutputDto :BaseOutputCommon
    {
        public int MedicalRecordId { get; set; }

        public int PatientId { get; set; }

        public DateTime MedicalRecordDate { get; set; }

        public string? ExamReason { get; set; }

        public string ExamCode { get; set; } = null!;

        public int ?DoctorId { get; set; }

        public virtual Doctor? Doctor { get; set; } = null!;

        public virtual ICollection<ExaminationResultId> ?ExaminationResultIds { get; set; }

        public virtual Patient Patient { get; set; } = null!;

        public virtual ICollection<Prescription> ?Prescriptions { get; set; } = new List<Prescription>();

        public virtual ICollection<Service> ?Services { get; set; } = new List<Service>();
    }
}
