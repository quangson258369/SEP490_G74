using AutoMapper;
using DataAccess.Entity;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Mapper
{
    public class DoctorMapper:Profile
    {
        private HcsContext context = new HcsContext();
        public DoctorMapper()
        {
            CreateMap<Employee, DoctorMRDTO>()
                .ForMember(x=>x.UserId,x=>x.MapFrom(x=>x.UserId))
                .ForMember(x=>x.ServiceTypeId,x=>x.MapFrom(x=>x.ServiceTypeId))
                .ForMember(x=>x.DoctorId,x=>x.MapFrom(x=>x.DoctorId))
                .ForMember(x=>x.Contact,x=>x.MapFrom(x=> getContact(x.DoctorId)));
            CreateMap<DoctorMRDTO, Employee> ();

        }

        private Contact getContact(int doctorId)
        {
            var doctor = context.Contacts.Where(x => x.DoctorId == doctorId).FirstOrDefault();
            return doctor;
        }
    }
}
