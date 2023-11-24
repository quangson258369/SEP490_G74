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
    public class ServiceMapper:Profile
    {
        public ServiceMapper() {
            CreateMap<Service, ServiceDTO>();
            CreateMap<ServiceDTO, Service>();
        }
    }
}
