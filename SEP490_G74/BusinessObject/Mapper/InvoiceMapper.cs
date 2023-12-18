using AutoMapper;
using DataAccess.Entity;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Mapper
{
    public class InvoiceMapper : Profile
    {
        private HcsContext context = new HcsContext();
        public InvoiceMapper()
        {
            CreateMap<InvoiceAdd, Invoice>();
            CreateMap<InvoiceDetailAdd, InvoiceDetail>();
            CreateMap<Invoice, InvoiceDTO>()
                .ForMember(x => x.Cashier, x => x.MapFrom(x => getCashier(x.CashierId)))
                .ForMember(x => x.Patient, x => x.MapFrom(x => getPatient(x.PatientId)))
                .ForMember(x => x.PatientId, x => x.MapFrom(x => x.PatientId))
                .ForMember(x => x.Status, x => x.MapFrom(x => x.Status))
                .ForMember(x => x.CashierId, x => x.MapFrom(x => x.CashierId))
                .ForMember(x => x.InvoiceId, x => x.MapFrom(x => x.InvoiceId))
                .ForMember(x => x.PaymentDate, x => x.MapFrom(x => x.PaymentDate))
                .ForMember(x=>x.CashierName, x=>x.MapFrom(x=> getCashierName(x.CashierId)))
                .ForMember(s=>s.CreateDate, s=>s.MapFrom(s=> GetDateCreate(s.InvoiceId)))
                .ForMember(x => x.PaymentMethod, x => x.MapFrom(x => x.PaymentMethod))
                .ForMember(x => x.InvoiceDetails, x => x.MapFrom(x => getListDetail(x.InvoiceId)));
        }

        private string getCashierName(int cashierId)
        {
            Employee u = context.Employees.SingleOrDefault(s => s.UserId == cashierId);
            Contact c = context.Contacts.Where(s => s.DoctorId == u.DoctorId).First();
            return c.Name;
        }

        private DateTime GetDateCreate(int invoiceId)
        {
            var InvoiceDetail = context.InvoiceDetails.Where(s=>s.InvoiceId ==  invoiceId).FirstOrDefault();
            if(InvoiceDetail != null)
            {
                var mr = context.MedicalRecords.Where(s => s.MedicalRecordId == InvoiceDetail.MedicalRecordId).FirstOrDefault();
                if(mr != null)
                {
                    return mr.MedicalRecordDate;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        private List<InvoiceDetail> getListDetail(int invoiceId)
        {
            return context.InvoiceDetails.Where(x => x.InvoiceId == invoiceId).ToList();
        }

        private PatientDTO? getPatient(int patientId)
        {
            Patient patient = context.Patients.SingleOrDefault(x => x.PatientId == patientId);
            PatientDTO p = new PatientDTO()
            {
                PatientId = patientId,
                Allergieshistory = patient.Allergieshistory,
                BloodGroup = patient.BloodGroup,
                BloodPressure = patient.BloodPressure,
                Height = patient.Height,
                ServiceDetailName = patient.ServiceDetailName,
                Weight = patient.Weight,
                Contacts = context.Contacts.Where(o => o.PatientId == patientId).OrderBy(s => s.PatientId).Last(),
                MedicalRecords = context.MedicalRecords.Where(o => o.PatientId == patientId).ToList(),
                Invoices = context.Invoices.Where(x => x.PatientId == patientId).ToList()
            };
            return p;
        }

        private User? getCashier(int cashierId)
        {
            return context.Users.SingleOrDefault(s => s.UserId == cashierId);

        }
    }
}
