using HcsBE.Common;

namespace HcsBE.Dao.Login
{
    public class LoginDao
    {
        public LoginDaoOutputDto GetUser(LoginDaoInputDto inputDto)
        {
            try
            {
                //var loginDao = this.DbContext;
                var output = new LoginDaoOutputDto();

                return output;
            }
            catch (Exception ex)
            {
                return new LoginDaoOutputDto()
                {
                    ExceptionMessage = ex.Message,
                    ResultCd = ConstantHcs.ExceptionStatus
                };
            }
        }
    }
}
