using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class PatientModify
    {
        public int PatientId { get; set; }

        public string? ServiceDetailName { get; set; }

        public byte? Height { get; set; }

        public byte? Weight { get; set; }

        public string? BloodGroup { get; set; }

        public byte? BloodPressure { get; set; }

        public string? Allergieshistory { get; set; }
    }
}
