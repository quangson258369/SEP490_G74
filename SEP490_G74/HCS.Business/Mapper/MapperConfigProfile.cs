using AutoMapper;
using HCS.Domain.Models;
using HCS.Business.RequestModel.CategoryRequestModel;
using HCS.Business.RequestModel.ContactRequestModel;
using HCS.Business.RequestModel.MedicalRecordRequestModel;
using HCS.Business.RequestModel.PatientRequestModel;
using HCS.Business.RequestModel.SuppliesTypeRequestModel;
using HCS.Business.ResponseModel.CategoryResponse;
using HCS.Business.ResponseModel.PatientResponseModel;
using HCS.Business.ResponseModel.SuppliesTypeResponseModel;

namespace HCS.Business.Mapper
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<PatientAddModel, Patient>();
            CreateMap<MedicalRecordAddModel, MedicalRecord>();
            CreateMap<ContactAddModel, Contact>();

            CreateMap<CategoryAddModel, Category>().ReverseMap();
            CreateMap<CategoryUpdateModel, Category>().ReverseMap();
            CreateMap<CategoryResponseModel, Category>().ReverseMap();
            
            CreateMap<PatientResponseModel, Patient>().ReverseMap();
            CreateMap<PatientAddModel, Patient>().ReverseMap();
            CreateMap<PatientUpdateModel, Patient>().ReverseMap();

            CreateMap<SuppliesTypeAddModel, SuppliesType>().ReverseMap();
            CreateMap<SuppliesTypeUpdateModel, SuppliesType>().ReverseMap();
            CreateMap<SuppliesTypeResponseModel, SuppliesType>().ReverseMap();

        }
    }
}
