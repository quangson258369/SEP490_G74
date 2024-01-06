﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public ICollection<User>? Doctors { get; set; }

        public ICollection<MedicalRecordCateogry>? MedicalRecordCategories { get; set; }

        public ICollection<ServiceType>? ServiceTypes { get; set; }
    }
}
