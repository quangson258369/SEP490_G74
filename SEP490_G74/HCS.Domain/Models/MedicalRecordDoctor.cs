﻿

namespace HCS.Domain.Models
{
    public class MedicalRecordDoctor
    {
        public int MedicalRecordId { get; set; }

        public MedicalRecord MedicalRecord { get; set; } = null!;

        public int DoctorId { get; set; }

        public User Doctor { get; set; } = null!;
    }
}
