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
            CreateMap<ContactPatientDTO, Contact>()
                .ForMember(x=>x.CId, x=>x.MapFrom(x=>x.CId))
                .ForMember(x=>x.PatientId, x=>x.MapFrom(x=>x.PatientId))
                .ForMember(x=>x.Address, x=>x.MapFrom(x=>x.Address))
                .ForMember(x=>x.Dob, x=>x.MapFrom(x=>x.Dob))
                .ForMember(x=>x.Gender, x=>x.MapFrom(x=>x.Gender))
                .ForMember(x=>x.Name, x=>x.MapFrom(x=>x.Name))
                .ForMember(x=>x.Img, x=>x.MapFrom(x=>x.Img))
                .ForMember(x=>x.Phone, x=>x.MapFrom(x=>x.Phone));
            CreateMap<Contact, ContactPatientDTO>()
                .ForMember(x => x.CId, x => x.MapFrom(x => x.CId))
                .ForMember(x => x.PatientId, x => x.MapFrom(x => x.PatientId))
                .ForMember(x => x.Address, x => x.MapFrom(x => x.Address))
                .ForMember(x => x.Dob, x => x.MapFrom(x => x.Dob))
                .ForMember(x => x.Gender, x => x.MapFrom(x => x.Gender))
                .ForMember(x => x.Name, x => x.MapFrom(x => x.Name))
                .ForMember(x => x.Img, x => x.MapFrom(x => x.Img))
                .ForMember(x => x.Phone, x => x.MapFrom(x => x.Phone));

            CreateMap<PatientModify, PatientDTO>()
                .ForMember(x => x.PatientId, x => x.MapFrom(x => x.PatientId))
                .ForMember(x => x.ServiceDetailName, x => x.MapFrom(x => x.ServiceDetailName));
            // sửa thành pid
            CreateMap<Patient, PatientDTO>()

                .ForMember(x => x.Contacts, x => x.MapFrom(x => getContact(x.PatientId)))
                .ForMember(x => x.Allergieshistory, x => x.MapFrom(x => x.Allergieshistory))
                .ForMember(x => x.MedicalRecords, x => x.MapFrom(x => getMedicalRecords(x.PatientId)))
                .ForMember(x => x.Invoices, x => x.MapFrom(x => getListInvoice(x.PatientId)));
        }

        private List<Contact> blinkData(int? pid)
        {
            var contacts = context.Contacts.Where(x => x.PatientId == pid).ToList();
            return contacts;
        }

        private HcsContext context = new HcsContext();
        private List<Invoice> getListInvoice(int? patientId)
        {
            var list = context.Invoices.Where(o => o.PatientId == patientId).ToList();
            return list;
        }

        private List<MedicalRecord> getMedicalRecords(int? patientId)
        {
            var list = context.MedicalRecords.Where(o => o.PatientId == patientId).ToList();
            return list;
        }

        private Contact getContact(int? patientId)
        {
            var list = context.Contacts.Where(o => o.PatientId == patientId).OrderBy(s=>s.PatientId);
            return list.Last();
        }
    }
}
