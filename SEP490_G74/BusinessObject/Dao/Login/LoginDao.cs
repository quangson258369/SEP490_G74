using API.Common;
using API.Common.Entity;

namespace HcsBE.Dao.Login
{
    public class LoginDao
    {
        public LoginDaoOutputDto GetUser(LoginDaoInputDto inputDto)
        {
            try
            {
                var dbContext = new ApplicationDbContext();
                var output = new LoginDaoOutputDto();

                var loginDao = dbContext.Users.Where(
                    record => record.UserId.Equals(inputDto.Username)
                        && record.Password.Equals(inputDto.Password)
                    );
                if (loginDao == null) 
                {
                    return new LoginDaoOutputDto()
                    {
                        ExceptionMessage = ConstantHcs.LoginFailedMessage,
                        ResultCd = ConstantHcs.ExceptionStatus
                    };
                }
                output.Token = loginDao.ToList().First().UserId.ToString();

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
