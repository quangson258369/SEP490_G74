using AutoMapper;
using DataAccess.Entity;
using HcsBE.Dao.MedicalRecordDAO;
using HcsBE.Dao.ServiceDao;
using HcsBE.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Mapper
{
    public class ServiceMapper:Profile
    {
        private HcsContext context = new HcsContext();
        private ServiceDAO serviceDAO = new ServiceDAO();
        private MedicalRecordDao recordDao = new MedicalRecordDao();
        public ServiceMapper() {
            CreateMap<Service, ServiceDTO>()
                .ForMember(x=>x.price ,x=> x.MapFrom(y => y.Price));
            CreateMap<ServiceDTO, Service>();
            CreateMap<ServiceMedicalRecord, ServiceMRDTO>()
                .ForMember(x => x.PatientContact, x => x.MapFrom(x => GetPatientContact(x.MedicalRecordId)))
                .ForMember(x=>x.DoctorContact,x=>x.MapFrom(x=> GetDoctorContact(x.DoctorId)))
                .ForMember(x=>x.Service, x=>x.MapFrom(x=> serviceDAO.GetService(x.ServiceId)));
            CreateMap<ServiceMRDTO, ServiceMedicalRecord>();
        }

        private Contact GetPatientContact(int medicalRecordId)
        {
            MedicalRecord record = recordDao.GetMedicalRecord(medicalRecordId);


            int pid = record.PatientId;
            Console.WriteLine("pid lay ra"+pid);
            return context.Contacts.SingleOrDefault(s => s.PatientId == pid);
        }

        private Contact GetDoctorContact(int? doctorId)
        {
            return context.Contacts.SingleOrDefault(s => s.DoctorId == doctorId);
        }
    }
}
