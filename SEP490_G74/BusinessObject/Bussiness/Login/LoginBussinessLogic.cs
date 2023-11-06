using API.Common;
using API.Common.Entity;
using AutoMapper;
using HcsBE.Dao.Login;

namespace HcsBE.Bussiness.Login
{
    public class LoginBussinessLogic
    {
        public LoginOutputDto Login(LoginInputDto inputDto)
        {
            try
            {
                var output = new LoginOutputDto();
                output.ResultCd = ConstantHcs.Success;
                LoginDao loginDao = new LoginDao();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingData());
                });

                var mapper = config.CreateMapper();
                var loginDaoInput = mapper.Map<LoginInputDto, LoginDaoInputDto>(inputDto);

                var loginDaoOutput = loginDao.GetUser(loginDaoInput);

                // Validate user not found
                if(loginDaoOutput.UserInfoDto == null)
                {
                    return new LoginOutputDto
                    {
                        ExceptionMessage = ConstantHcs.LoginFailedMessage,
                        ResultCd = ConstantHcs.BussinessError,
                    };
                }

                

                output.UserInfoDto = mapper.Map<User, UserInfo> (loginDaoOutput.UserInfoDto);

                return output;
            }catch (Exception ex)
            {
                return new LoginOutputDto
                {
                    ExceptionMessage = ex.Message,
                    ResultCd = ConstantHcs.ExceptionStatus,
                };
            }
        }
    }
}
