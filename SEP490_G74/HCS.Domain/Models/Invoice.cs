﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public bool Status { get; set; }

        public decimal Total { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public int PatientId { get; set; }

        public Patient Patient { get; set; } = null!;

        public int CashierId { get; set; }

        public User Cashier { get; set; } = null!;

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

        
    }
}
