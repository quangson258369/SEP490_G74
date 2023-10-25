using System;
using System.Collections.Generic;

namespace API.Common.Entity;

public partial class Patient
{
    public int PatientId { get; set; }

    public string ServiceDetailName { get; set; } = null!;

    public DateTime ExamDate { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
