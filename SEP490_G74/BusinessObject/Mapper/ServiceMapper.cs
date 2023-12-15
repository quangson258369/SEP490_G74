using AutoMapper;
using DataAccess.Entity;
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
        public ServiceMapper() {
            CreateMap<Service, ServiceDTO>()
                .ForMember(x=>x.price ,x=> x.MapFrom(y => y.Price))
                .ForMember(x=>x.ServiceTypeName,x=>x.MapFrom(y=>getTypeName(y.ServiceTypeId)));
            CreateMap<ServiceDTO, Service>();
            CreateMap<ServiceMedicalRecord, ServiceMRDTO>()
                .ForMember(x=>x.DoctorContact,x=>x.MapFrom(x=> GetDoctorContact(x.DoctorId)))
                .ForMember(x=>x.Service, x=>x.MapFrom(x=> serviceDAO.GetService(x.ServiceId)));
            CreateMap<ServiceMRDTO, ServiceMedicalRecord>();
        }

        private Contact GetDoctorContact(int? doctorId)
        {
            return context.Contacts.SingleOrDefault(s => s.DoctorId == doctorId);
        }
        private string getTypeName(int id)
        {
            return context.ServiceTypes.Where(x=>x.ServiceTypeId==id).Select(x=>x.ServiceTypeName).FirstOrDefault();
        }
    }
}
