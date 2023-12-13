using AutoMapper;
using DataAccess.Entity;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.ServiceBusiness
{
    public class ServiceMapper :Profile
    {
        private HcsContext context = new HcsContext();
        public ServiceMapper() 
        {
            CreateMap<ServiceDTO, Service>();
            CreateMap<Service, ServiceDTO>()
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ServiceTypeName, opt => opt.MapFrom(src => getTypeServiceName(src.ServiceTypeId)));
        }
        public string getTypeServiceName(int? id)
        {
            if (id == null)
            {
                return "not info";
            }
            var c = context.ServiceTypes.Where(o => o.ServiceTypeId == id).FirstOrDefault();

            return c.ServiceTypeName;
        }
    }
}
