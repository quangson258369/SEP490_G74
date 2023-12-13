using AutoMapper;
using DataAccess.Entity;
using HcsBE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Bussiness.PrescriptionBusiness
{
    public class PrescriptionMapper:Profile
    {
        public PrescriptionMapper()
        {
            CreateMap<Prescription, PrescriptionDTO>()
            .ForMember(dest => dest.PrescriptionId, opt => opt.MapFrom(src => src.PrescriptionId))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
            .ForMember(dest => dest.Diagnose, opt => opt.MapFrom(src => src.Diagnose));
        }
    }
}
