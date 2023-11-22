using AutoMapper;
using DataAccess.Entity;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.Patient
{
    public class PatientMapper:Profile
    {
        public PatientMapper() {
            CreateMap<PatientDTO, DataAccess.Entity.Patient>();
            CreateMap<PatientModify, DataAccess.Entity.Patient>();
            CreateMap<DataAccess.Entity.Patient, PatientDTO>()
                .ForMember(x=>x.Contacts,x=>x.MapFrom(x=> getContact(x.PatientId)))
                .ForMember(x=>x.MedicalRecords,x=>x.MapFrom(x=> getMedicalRecords(x.PatientId)))
                .ForMember(x=>x.Invoices,x=>x.MapFrom(x=> getListInvoice(x.PatientId)));
        }
        private HcsContext context = new HcsContext();
        private List<Invoice> getListInvoice(int? patientId)
        {
            if(patientId == null)
            {
                return null;
            }
            var list = context.Invoices.Where(o=>o.PatientId ==patientId).ToList();
            return list;
        }

        private List<DataAccess.Entity.MedicalRecord> getMedicalRecords(int? patientId)
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
