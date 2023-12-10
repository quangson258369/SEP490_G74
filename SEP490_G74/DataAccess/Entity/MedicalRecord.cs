using System;
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

    public int? PrescriptionId { get; set; }
    [JsonIgnore]
    public virtual Employee Doctor { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<ExaminationResultId> ExaminationResultIds { get; set; } = new List<ExaminationResultId>();
    [JsonIgnore]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    [JsonIgnore]
    public virtual Patient Patient { get; set; } = null!;
    [JsonIgnore]
    public virtual Prescription? Prescription { get; set; }
    [JsonIgnore]
    public virtual ICollection<ServiceMedicalRecord> ServiceMedicalRecords { get; set; } = new List<ServiceMedicalRecord>();
}
