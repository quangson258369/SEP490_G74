using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class PrescriptionDTO
    {
        public int PrescriptionId { get; set; }

        public DateTime CreateDate { get; set; }

        public string Diagnose { get; set; } = null!;

        public byte Quantity { get; set; }

        public int MedicalRecordId { get; set; }
    }
}
