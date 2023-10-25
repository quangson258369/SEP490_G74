using HcsBE.Common;

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
