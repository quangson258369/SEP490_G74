using API.Common;
using API.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.MedicalRecordDAO
{
    public class MedicalRecordDaoOutputDto:BaseOutputCommon
    {
        public MedicalRecord medicalRecordDto { get; set; }
        public ExaminationResultId ExaminationResultDTO { get; set; }
        public Patient Patient { get; set; }
        public Prescription Prescription { get; set; }
        public Service Service { get; set; }
    }
}
