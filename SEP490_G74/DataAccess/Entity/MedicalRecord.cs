using System;
using System.Collections.Generic;

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

    public virtual Employee Doctor { get; set; } = null!;

    public virtual ICollection<ExaminationResultId> ExaminationResultIds { get; set; } = new List<ExaminationResultId>();

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual Prescription? Prescription { get; set; }

    public virtual ICollection<ServiceMedicalRecord> ServiceMedicalRecords { get; set; } = new List<ServiceMedicalRecord>();
}
