using API.Common;
using HcsBE.Bussiness.Login;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Login
{
    [Route("api/login")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        [HttpPost]
        public LoginOutputDto LoginApi(LoginInputDto inputDto)
        {
            var bussinessLogic = new LoginBussinessLogic().Login(inputDto);
            

            return bussinessLogic;
        }
    }
}
