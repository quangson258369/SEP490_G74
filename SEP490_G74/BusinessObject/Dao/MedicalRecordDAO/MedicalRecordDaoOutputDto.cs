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
    }
}
