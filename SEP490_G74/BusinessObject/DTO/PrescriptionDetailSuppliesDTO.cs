using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class PrescriptionDetailSuppliesDTO
    {
        public int PrescriptionId { get; set; }

        public DateTime CreateDate { get; set; }

        public string Diagnose { get; set; } = null!;

        public int SId { get; set; }
        public string SuppliesName { get; set; } = null!;
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public string SuppliesTypeName { get; set; } = null!;
    }
}
