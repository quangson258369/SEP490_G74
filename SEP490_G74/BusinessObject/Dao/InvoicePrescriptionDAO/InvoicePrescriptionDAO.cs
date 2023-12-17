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
        public string AddInvoicePrescriptionAsync(List<PrescriptionDetailSuppliesDTO> listSuppliesInPrescription, int invoiceIdToAdd, int invoidetailId)
        {
            decimal total = 0;
            foreach (var item in listSuppliesInPrescription)
            {
                total += (item?.Price ?? 0) * (item?.Quantity ?? 0);
            }

            //var query = @"SELECT TOP 1 U.UserId AS CashierId FROM [User] U
            //    JOIN Invoice I ON U.UserId = I.CashierId
            //    JOIN UserRole UR ON U.UserId = UR.UserId
            //    WHERE I.Status = 0 AND UR.RoleId = 3
            //    GROUP BY U.UserId
            //    HAVING COUNT(I.InvoiceId) = (
            //    SELECT MIN(InvoiceCount)
            //    FROM (
            //    SELECT COUNT(InvoiceId) AS InvoiceCount
            //    FROM Invoice
            //    WHERE Status = 0
            //    GROUP BY CashierId
            //    ) AS InvoiceCounts
            //    )";
            //var result = context.Users.FromSql(query);
            //int count = Convert.ToInt32(result);

            var newInvoice = new Invoice
            {
                PatientId = invoiceIdToAdd,
                CashierId = 7,
                PaymentDate = new DateTime(2023, 12, 12),
                Status = false,
                PaymentMethod = "Chưa mua",
                Total = total
            };
            var rowInvoice = context.Invoices.Add(newInvoice);
            context.SaveChanges();
            var newInvoiceDetail = new InvoiceDetail
            {
                InvoiceDetailId = context.InvoiceDetails.Max(x => x.InvoiceDetailId) + 1,
                InvoiceId = context.Invoices.Max(x => x.InvoiceId),
                IsPrescription = true,
                MedicalRecordId = invoidetailId
            };
            var rowInvoiceDetail = context.InvoiceDetails.Add(newInvoiceDetail);
            context.SaveChanges();
            return "Oke";
        }
    }
}
