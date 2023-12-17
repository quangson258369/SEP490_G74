using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class InvoiceAdd
    {
        public int PatientId { get; set; }

        public int CashierId { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool Status { get; set; }

        public decimal Total { get; set; }

        public string PaymentMethod { get; set; } = null!;
    }
}
