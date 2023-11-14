using API.Common.Entity;
using AutoMapper;
using HcsBE.Dao.Login;

namespace HcsBE.Bussiness.Login
{
    public class LoginDataConversion
    {
        public static void CreateMap(IMapperConfigurationExpression cfg)
        {
            // config syntax => cfg.CreateMap<OriginDto, DestinationDto>();
            cfg.CreateMap<LoginInputDto, LoginDaoInputDto>();
            cfg.CreateMap<User, UserInfo>();
        }
    }

    public class MappingData : Profile
    {
        public MappingData()
        {
            CreateMap<LoginInputDto, LoginDaoInputDto>();
            CreateMap<User, UserInfo>();
        }
    }
}
