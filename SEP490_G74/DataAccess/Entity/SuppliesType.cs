﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class SuppliesType
{
    public int SuppliesTypeId { get; set; }

    public string SuppliesTypeName { get; set; } = null!;

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}