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
using Microsoft.Data.SqlClient;

namespace HcsBE.Mapper
{
    public class MedicalRCMapper : Profile
    {
        private HcsContext context = new HcsContext();
        public MedicalRCMapper()
        {
            CreateMap<MedicalRecordModify, MedicalRecord>();
            CreateMap<MedicalRecord, MedicalRecordModify>()
                .ForMember(x=>x.PrescriptionId,x=>x.MapFrom(x=>x.PrescriptionId));
            CreateMap<ExaminationResultId, ExaminationResultIdMRDTO>();
            CreateMap<Contact,ContactDoctorDTO>();
            CreateMap<Contact,ContactPatientDTO>();
            CreateMap<MedicalRecordDaoOutputDto, MedicalRecord>();
            CreateMap<MedicalRecord, MedicalRecordDaoOutputDto>()
                .ForMember(o => o.PatientName, opt => opt.MapFrom(o => getName(o.PatientId)))
                .ForMember(o => o.PatientPhone, opt => opt.MapFrom(o => getPhones(o.PatientId)))
                .ForMember(o => o.ExaminationResultIds, o => o.MapFrom(a => ListExamRS(a.MedicalRecordId)))
                .ForMember(o =>o.Doctor, o => o.MapFrom(x=> GetDoctor(x.DoctorId)))
                .ForMember(o =>o.Patient, o => o.MapFrom(x=> GetPatient(x.PatientId)))
                .ForMember(o =>o.Services, o => o.MapFrom(x=> ListService(x.MedicalRecordId)))
                .ForMember(o=>o.InvoiceDetails, o => o.MapFrom(x=> ListInvoiceDetail(x.MedicalRecordId)))
                .ForMember(o => o.ServiceMedicalRecords, o => o.MapFrom(x=> ListServiceMedicalRecord(x.MedicalRecordId)))
                ;
        }

        private List<ServiceMedicalRecord> ListServiceMedicalRecord(int medicalRecordId)
        {
            var rs = context.ServiceMedicalRecords.Where(x => x.MedicalRecordId == medicalRecordId).ToList();
            return rs;
        }

        private List<InvoiceDetail> ListInvoiceDetail(int medicalRecordId)
        {
            var rs = context.InvoiceDetails.Where(x => x.MedicalRecordId == medicalRecordId).ToList();
            return rs;
        }

        private List<Service> ListService(int? medicalRecordId)
        {
            if (medicalRecordId == null) return null;
            var rs = context.Services.FromSqlRaw("select s.serviceId,s.serviceName,s.serviceTypeId, s.price from [Service] s " +
                "join ServiceMedicalRecord sm on s.serviceId = sm.serviceId " +
                "join MedicalRecord mr on sm.medicalRecordId = mr.medicalRecordID " +
                "where mr.medicalRecordID = @paramName",
                new SqlParameter("@paramName", medicalRecordId)).ToList();
            return rs;
        }

        private Patient GetPatient(int? patientId)
        {
            if (patientId == null) return null;
            var doctor = context.Patients.Where(x => x.PatientId == patientId).FirstOrDefault();
            Patient d = new Patient()
            {
                PatientId = doctor.PatientId,
                ServiceDetailName = doctor.ServiceDetailName,
                BloodGroup = doctor.BloodGroup,
                BloodPressure = doctor.BloodPressure,
                Height = doctor.Height,
                Weight = doctor.Weight,
                Allergieshistory = doctor.Allergieshistory,
                Invoices = context.Invoices.Where(x => x.PatientId == patientId).ToList(),
                Contacts =context.Contacts.Where(x => x.PatientId == doctor.PatientId).ToList()
            };
            return d;
        }

        private Employee GetDoctor(int? doctorId)
        {
            if (doctorId == null) return null;
            var doctor = context.Employees.Where(x=>x.DoctorId == doctorId).FirstOrDefault();
            Employee d = new Employee()
            {
                DoctorId = doctor.DoctorId,
                ServiceTypeId = doctor.ServiceTypeId,
                UserId = doctor.UserId,
                ServiceMedicalRecords = context.ServiceMedicalRecords.Where(x=>x.DoctorId==doctorId).ToList(),
                ServiceType = context.ServiceTypes.SingleOrDefault(x=>x.ServiceTypeId == doctor.ServiceTypeId),
                Contacts = context.Contacts.Where(x=>x.DoctorId == doctor.DoctorId).ToList(),
                User = context.Users.SingleOrDefault(x=>x.UserId == doctor.UserId) == null? null : context.Users.SingleOrDefault(x => x.UserId == doctor.UserId)
            };
            return d;
        }

        private List<ExaminationResultId> ListExamRS(int? medicalRecordId)
        {
            if (medicalRecordId == null) return null;
            var rs = context.ExaminationResultIds.Where(x => x.MedicalRecordId == medicalRecordId).ToList();
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
