﻿using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.DTO
{
    public class SuppliesDetailDTO
    {
        public int SId { get; set; }
        public string? SName { get; set; }
        public int UnitInStock { get; set; }
        public DateTime? InputDay { get; set; }
        public DateTime Exp { get; set; }
        public decimal Price { get; set; }
        public string? Distributor { get; set; }
        public string? Uses { get; set; }
        public SuppliesType? SuppliesType { get; set; }
    }
}