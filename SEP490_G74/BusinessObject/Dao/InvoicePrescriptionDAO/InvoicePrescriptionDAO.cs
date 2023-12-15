using DataAccess.Entity;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.InvoicePrescriptionDAO
{
    public class InvoicePrescriptionDAO
    {
        private HcsContext context = new HcsContext();
        public List<InvoicePrescriptionDTO> GetListInvoicePrescription(int page = 1)
        {
            int pageSize = 3;
            var result = context.Invoices
                .Join(context.InvoiceDetails, i => i.InvoiceId, id => id.InvoiceId, (i, id) => new { i, id })
                .Join(context.Patients, data => data.i.PatientId, p => p.PatientId, (data, p) => new { data.i, data.id, p })
                .Join(context.Contacts, data => data.p.PatientId, c => c.PatientId, (data, c) => new { data.i, data.id, data.p, c })
                .Join(context.MedicalRecords, data => data.id.MedicalRecordId, mr => mr.MedicalRecordId, (data, mr) => new { data.i, data.p, data.c, mr })
                .Join(context.Prescriptions, data => data.mr.PrescriptionId, pr => pr.PrescriptionId, (data, pr) => new { data.i, data.c, data.mr, pr })
                .Select(result => new InvoicePrescriptionDTO
                    {
                    InvoiceId = result.i.InvoiceId,
                    NamePatient = result.c.Name,
                    PhonePatient = result.c.Phone,
                    CreateDate = result.pr.CreateDate,
                    PaymentDate = result.i.PaymentDate,
                    Status = result.i.Status
                    })
                    .ToList();
            var pagedData = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedData;
        }
        public int GetCountListInvoicePrescription()
        {
            var result = context.Invoices
            .Join(context.Patients, invoice => invoice.PatientId, patient => patient.PatientId, (invoice, patient) => new { invoice, patient })
            .Join(context.Contacts, data => data.patient.PatientId, contact => contact.PatientId, (data, contact) => new { data.invoice, contact })
            .Join(context.MedicalRecords, data => data.invoice.PatientId, medicalRecord => medicalRecord.PatientId, (data, medicalRecord) => new { data.invoice, data.contact, medicalRecord })
            .Join(context.Prescriptions, data => data.medicalRecord.PrescriptionId, prescription => prescription.PrescriptionId, (data, prescription) => new { data.invoice, data.contact, data.medicalRecord, prescription })
            .Select(result => new InvoicePrescriptionDTO
            {
                InvoiceId = result.invoice.InvoiceId,
                NamePatient = result.contact.Name,
                PhonePatient = result.contact.Phone,
                CreateDate = result.prescription.CreateDate,
                PaymentDate = result.invoice.PaymentDate,
                Status = result.invoice.Status
            })
            .ToList();
            int count = result.Count();
            return count;
        }
        public bool addInvoicePrescription(List<PrescriptionDetailSuppliesDTO> listSuppliesInPrescription)
        {

            return true;
        }
    }
}
