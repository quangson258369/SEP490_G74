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
        public IActionResult LoginApi(LoginInputDto inputDto)
        {
            var bussinessLogic = new LoginBussinessLogic().Login(inputDto);
            if (bussinessLogic.ResultCd.HasValue &&
                (bussinessLogic.ResultCd.Value == ConstantHcs.BussinessError
                || bussinessLogic.ResultCd.Value == ConstantHcs.ExceptionStatus))
            {
                return NotFound();
            }


            return Ok(bussinessLogic);
        }
    }
}
