﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Models
{
    public class Supply
    {
        public int SId { get; set; }

        public string SName { get; set; } = null!;

        public string Uses { get; set; } = null!;

        public DateTime Exp { get; set; }

        public string Distributor { get; set; } = null!;

        public short UnitInStock { get; set; }

        public double Price { get; set; }

        public DateTime Inputday { get; set; } = DateTime.Now;

        public int SuppliesTypeId { get; set; }

        public SuppliesType SuppliesType { get; set; } = null!;

        public ICollection<SuppliesPrescription> SuppliesPrescriptions { get; set; } = new List<SuppliesPrescription>();
        
    }
}
