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
                    record => record.Email.Equals(inputDto.Email)
                        && record.Password.Equals(inputDto.Password)
                    );
                var loginDao2 = from user in dbContext.Users
                                from role in dbContext.Roles
                                where role.Users.Any(r => r.UserId == user.UserId)
                                where user.Email.Equals(inputDto.Email)
                                && user.Password.Equals(inputDto.Password)
                                select new { user, role };
                
                // Validate empty list
                if (!loginDao.Any()) 
                {
                    return new LoginDaoOutputDto()
                    {
                        ExceptionMessage = ConstantHcs.LoginFailedMessage,
                        ResultCd = ConstantHcs.ExceptionStatus
                    };
                }

                output.UserInfoDto = loginDao.ToList().First();

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
