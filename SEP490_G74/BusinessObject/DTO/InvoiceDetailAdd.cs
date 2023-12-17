using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class InvoiceDetailAdd
    {
        public int InvoiceDetailId { get; set; }

        public int InvoiceId { get; set; }

        public int MedicalRecordId { get; set; }

        public bool IsPrescription { get; set; }
    }
}
