using API.Common;
using HcsBE.Bussiness.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers.Login
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string _token;
        public LoginController(IConfiguration configuration) 
        {
            _token = configuration.GetSection("Jwt:Key").Value;
        }

        [HttpPost("login")]
        public LoginOutputDto LoginApi(LoginInputDto inputDto)
        {
            var bussinessLogic = new LoginBussinessLogic();
            var result = bussinessLogic.Login(inputDto);
            if (result.UserInfoDto != null)
            {
                
                result.KeyDto.Key = GenerateToken(result.UserInfoDto);
            }

            return result;
        }       
        private string GenerateToken(UserInfo inputDto)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            
            var jwtKeyBytes = Encoding.UTF8.GetBytes(_token);


            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, inputDto.Email ?? "GUEST"),
                    new Claim(ClaimTypes.NameIdentifier, inputDto.UserId ?? "GUEST"),
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDesc);

            return jwtTokenHandler.WriteToken(token);
        }
        [HttpPost("CheckUserName")]
        public bool CheckUserName(string username)
        {
            var bussinessLogic = new LoginBussinessLogic();
            var result = bussinessLogic.CheckUserName(username);
            return result;
        }
        //UpdatePassword
        [HttpPost("UpdatePassword")]
        public bool UpdatePassword(string username,string newPass)
        {
            var bussinessLogic = new LoginBussinessLogic();
            var result = bussinessLogic.UpdatePassword(username,newPass);
            return result;
        }
    }
}
