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
        private ServiceDAO dao = new ServiceDAO();
        public ServiceMapper() {
            CreateMap<Service, ServiceDTO>()
                .ForMember(x=>x.price ,x=> x.MapFrom(y => y.Price));
            CreateMap<ServiceDTO, Service>();
        }

    }
}
