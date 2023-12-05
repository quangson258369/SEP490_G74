using API.Common;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class ServiceMRDTO:BaseOutputCommon
    {
        public int ServiceId { get; set; }

        public int MedicalRecordId { get; set; }

        public int? DoctorId { get; set; }

        public Service? Service { get; set; }

        public Contact? DoctorContact { get; set; }

    }
}
