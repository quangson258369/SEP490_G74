using DataAccess.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using HcsBE.DTO;

namespace HcsBE.Bussiness.MedicalRecord
{
    public class MedicalRCMapper:Profile
    {
        private HcsContext context = new HcsContext();
        public MedicalRCMapper() {
            CreateMap<MedicalRecordDaoOutputDto, DataAccess.Entity.MedicalRecord>();
            CreateMap<DataAccess.Entity.MedicalRecord, MedicalRecordDaoOutputDto>()
                .ForMember(o => o.PatientName, opt => opt.MapFrom(o => getName(o.PatientId)))
                .ForMember(o => o.PatientPhone, opt => opt.MapFrom(o => getPhones(o.PatientId)))
                .ForMember(o =>o.ExaminationResultIds,o => o.MapFrom(a=> ListExamRS(a.MedicalRecordId)));
        }

        private List<ExaminationResultId> ListExamRS(int? medicalRecordId)
        {
            if (medicalRecordId == null) return null;
            var rs = context.ExaminationResultIds.Where(x=>x.MedicalRecordId == medicalRecordId).ToList();
            return rs;
        }

        public string getName(int? id)
        {
            if (id == null)
            {
                return "not info";
            }
            var c = context.Contacts.Where(o => o.PatientId == id).FirstOrDefault();

            return c.Name;
        }
        public string getPhones(int? id)
        {
            if (id == null)
            {
                return "not info";
            }
            var c = context.Contacts.Where(o => o.PatientId == id).FirstOrDefault();

            return c.Phone;
        }
    }
}
