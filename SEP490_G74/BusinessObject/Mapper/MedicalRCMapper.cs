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
using HcsBE.Bussiness.Login;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace HcsBE.Mapper
{
    public class MedicalRCMapper : Profile
    {
        private HcsContext context = new HcsContext();
        public MedicalRCMapper()
        {
            CreateMap<MedicalRecordModify, MedicalRecord>();
            CreateMap<MedicalRecord, MedicalRecordModify>();
            CreateMap<ExaminationResultId, ExaminationResultIdMRDTO>();
            CreateMap<Contact,ContactDoctorDTO>();
            CreateMap<Contact,ContactPatientDTO>();
            CreateMap<MedicalRecordDaoOutputDto, MedicalRecord>();
            CreateMap<MedicalRecord, MedicalRecordDaoOutputDto>()
                .ForMember(o => o.PatientName, opt => opt.MapFrom(o => getName(o.PatientId)))
                .ForMember(o => o.PatientPhone, opt => opt.MapFrom(o => getPhones(o.PatientId)))
                //.ForMember(o => o.ExaminationResultIds, o => o.MapFrom(a => ListExamRS(a.MedicalRecordId)))
                //.ForMember(o =>o.Doctor, o => o.MapFrom(x=> GetDoctor(x.DoctorId)))
                //.ForMember(o =>o.Patient, o => o.MapFrom(x=> GetPatient(x.PatientId)))
                //.ForMember(o =>o.Services, o => o.MapFrom(x=> ListService(x.MedicalRecordId)))
                ;
        }

        private List<ServiceDTO> ListService(int? medicalRecordId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MedicalRCMapper());
            });

            var mapper = config.CreateMapper();
            if (medicalRecordId == null) return null;
            var rs = context.Services.FromSqlRaw("select s.serviceId,s.serviceName,s.serviceTypeId from [Service] s " +
                "join ServiceMedicalRecord sm on s.serviceId = sm.serviceId " +
                "join MedicalRecord mr on sm.medicalRecordId = mr.medicalRecordID " +
                "where mr.medicalRecordID = @paramName",
                new SqlParameter("@paramName", medicalRecordId)).ToList();
            return mapper.Map<List<ServiceDTO>>(rs);
        }

        private PatientModify GetPatient(int? patientId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MedicalRCMapper());
            });

            var mapper = config.CreateMapper();
            if (patientId == null) return null;
            var doctor = context.Patients.Where(x => x.PatientId == patientId).FirstOrDefault();
            PatientModify d = new PatientModify()
            {
                PatientId = doctor.PatientId,
                ExamDate = doctor.ExamDate,
                ServiceDetailName = doctor.ServiceDetailName,
                Contact = mapper.Map<Contact, ContactPatientDTO>(context.Contacts.FirstOrDefault(x => x.PatientId == doctor.PatientId))
            };
            return d;
        }

        private DoctorMRDTO GetDoctor(int? doctorId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MedicalRCMapper());
            });

            var mapper = config.CreateMapper();
            if (doctorId == null) return null;
            var doctor = context.Doctors.Where(x=>x.DoctorId == doctorId).FirstOrDefault();
            DoctorMRDTO d = new DoctorMRDTO()
            {
                DoctorId = doctor.DoctorId,
                DoctorSpecialize = doctor.DoctorSpecialize,
                ServiceTypeId = doctor.ServiceTypeId,
                UserId = doctor.UserId,
                Contact = mapper.Map< Contact,ContactDoctorDTO>(context.Contacts.FirstOrDefault(x=>x.DoctorId == doctor.DoctorId))
            };
            return d;
        }

        private List<ExaminationResultIdMRDTO> ListExamRS(int? medicalRecordId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MedicalRCMapper());
            });

            var mapper = config.CreateMapper();
            if (medicalRecordId == null) return null;
            var rs = context.ExaminationResultIds.Where(x => x.MedicalRecordId == medicalRecordId).ToList();
            return mapper.Map<List<ExaminationResultIdMRDTO>>(rs);
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
