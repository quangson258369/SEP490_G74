using DataAccess.Entity;
using AutoMapper;
using HcsBE.Dao.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using API.Common;

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
                if (loginDaoOutput.UserInfoDto == null)
                {
                    return new LoginOutputDto
                    {
                        ExceptionMessage = ConstantHcs.LoginFailedMessage,
                        ResultCd = ConstantHcs.BussinessError,
                    };
                }



                output.UserInfoDto = mapper.Map<User, UserInfo>(loginDaoOutput.UserInfoDto);


                return output;
            }
            catch (Exception ex)
            {
                return new LoginOutputDto
                {
                    ExceptionMessage = ex.Message,
                    ResultCd = ConstantHcs.ExceptionStatus,
                };
            }
        }
        public bool CheckUserName(string username)
        {
            var output = new LoginDao();
            return output.CheckUserName(username);
        }
        //UpdatePassword
        public bool UpdatePassword(string username,string newPass)
        {
            var output = new LoginDao();
            return output.UpdatePassword(username,newPass);
        }
    }
}
