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
    public class PatientMapper : Profile
    {
        public PatientMapper()
        {

            CreateMap<PatientModify, Patient>();
            CreateMap<ContactPatientDTO, Contact>();

            CreateMap<PatientModify, PatientDTO>()
                .ForMember(x => x.PatientId, x => x.MapFrom(x => x.PatientId))
                .ForMember(x => x.ServiceDetailName, x => x.MapFrom(x => x.ServiceDetailName))
                .ForMember(x => x.Contacts, x => x.MapFrom(x => x.Contact));
            CreateMap<PatientDTO, PatientModify>()
                .ForMember(x => x.PatientId, x => x.MapFrom(x => x.PatientId))
                .ForMember(x => x.ServiceDetailName, x => x.MapFrom(x => x.ServiceDetailName))
                .ForMember(x => x.Contact, x => x.MapFrom(x => x.Contacts));

            CreateMap<PatientDTO, Patient>()
                .ForMember(x => x.Contacts, x => x.MapFrom(x => blinkData(x.Contacts)));
            CreateMap<Patient, PatientDTO>()
                .ForMember(x => x.Contacts, x => x.MapFrom(x => getContact(x.PatientId)))
                .ForMember(x => x.MedicalRecords, x => x.MapFrom(x => getMedicalRecords(x.PatientId)))
                .ForMember(x => x.Invoices, x => x.MapFrom(x => getListInvoice(x.PatientId)));
        }

        private List<Contact> blinkData(Contact? contacts)
        {
            var blink = new List<Contact>();
            context.Contacts.Where(x=>x.PatientId == contacts.PatientId);
            
            blink.Add(contacts);
            return blink;
        }

        private HcsContext context = new HcsContext();
        private List<Invoice> getListInvoice(int? patientId)
        {
            if (patientId == null)
            {
                return null;
            }
            var list = context.Invoices.Where(o => o.PatientId == patientId).ToList();
            return list;
        }

        private List<MedicalRecord> getMedicalRecords(int? patientId)
        {
            if (patientId == null)
            {
                return null;
            }
            var list = context.MedicalRecords.Where(o => o.PatientId == patientId).ToList();
            return list;
        }

        private Contact getContact(int? patientId)
        {
            if (patientId == null)
            {
                return null;
            }
            var list = context.Contacts.SingleOrDefault(o => o.PatientId == patientId);
            return list;
        }
    }
}
