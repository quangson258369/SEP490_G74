﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entity;

public partial class ExaminationResultId
{
    public int ExamResultId { get; set; }

    public int MedicalRecordId { get; set; }

    public int DoctorId { get; set; }

    public string Diagnosis { get; set; } = null!;

    public string Conclusion { get; set; } = null!;

    public DateTime ExamDate { get; set; }

    public int ServiceId { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}