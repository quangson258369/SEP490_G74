using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class InvoicePrescriptionDTO
    {
        public int InvoiceId { get; set; }
        public string NamePatient { get; set; } = null!;
        public string PhonePatient { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool Status { get; set; }
    }
}
