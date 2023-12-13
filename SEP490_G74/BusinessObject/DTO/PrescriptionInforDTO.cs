using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class PrescriptionInforDTO
    {
        public int PrescriptionId { get; set; }

        public DateTime CreateDate { get; set; }

        public string Diagnose { get; set; } = null!;

        public string NamePatient { get; set; } = null!;

        public string PhonePatient { get; set; } = null!;

    }
}
