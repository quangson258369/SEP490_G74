﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Domain.Models
{
    public class ServiceMedicalRecord
    {
        public int ServiceId { get; set; }

        public Service Service { get; set; } = null!;

        public int MedicalRecordId { get; set; }

        public MedicalRecord MedicalRecord { get; set; } = null!;

        public bool? Status { get; set; }
    }
}
