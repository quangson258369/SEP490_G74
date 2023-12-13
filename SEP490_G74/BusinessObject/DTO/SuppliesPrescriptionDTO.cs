using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class SuppliesPrescriptionDTO
    {
        public int SId { get; set; }

        public int PrescriptionId { get; set; }

        public int? Quantity { get; set; }
    }
}
