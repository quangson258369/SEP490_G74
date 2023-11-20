﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataAccess.Entity;

public partial class MedicalRecord
{
    public int MedicalRecordId { get; set; }

    public int PatientId { get; set; }

    public DateTime MedicalRecordDate { get; set; }

    public string? ExamReason { get; set; }

    public string ExamCode { get; set; } = null!;

    public int DoctorId { get; set; }
    [JsonIgnore]
    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<ExaminationResultId> ExaminationResultIds { get; set; } = new List<ExaminationResultId>();
    [JsonIgnore]
    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}