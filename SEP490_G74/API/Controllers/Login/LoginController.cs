using API.Common;
using HcsBE.Bussiness.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers.Login
{
    
    [ApiController]
    public class LoginController : ControllerBase
    {
        
        [Route("api/login")]
        [HttpPost]
        public LoginOutputDto LoginApi(LoginInputDto inputDto)
        {
            var bussinessLogic = new LoginBussinessLogic().Login(inputDto);
            
            return bussinessLogic;
        }
    }
}
