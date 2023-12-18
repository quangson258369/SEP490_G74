using API.Common;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class InvoiceDTO:BaseOutputCommon
    {
        public int InvoiceId { get; set; }

        public int PatientId { get; set; }

        public int CashierId { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool Status { get; set; }

        public decimal Total { get; set; }

        public DateTime CreateDate { get; set; }
        public string? PaymentMethod { get; set; }
        public User? Cashier { get; set; }
        public List<InvoiceDetail>? InvoiceDetails { get; set; }
        public PatientDTO? Patient { get; set; }
    }
}
