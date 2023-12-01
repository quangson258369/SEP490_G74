using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class SuppliesDTO
    {
        public int SId { get; set; }
        public string? SName { get; set; }
        public int UnitInStock { get; set; }
        public SuppliesType? suppliesType { get; set; }
    }
}
