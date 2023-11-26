using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class SuppliesPagination
    {
        public List<SuppliesDTO>? supplies { get; set; }
        public int TotalItemCount { get; set; }
        public int PageNumber { get; set; }
    }
}
