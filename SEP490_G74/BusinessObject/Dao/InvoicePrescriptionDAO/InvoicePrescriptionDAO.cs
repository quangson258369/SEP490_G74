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
        public List<InvoicePrescriptionDTO> GetListInvoicePrescription(int page=1) 
        {
            int pageSize = 3;
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
    }
}
