using API.Common;
using DataAccess.Entity;
using HcsBE.Dao.GenPassword;
using System.Security.Claims;

namespace HcsBE.Dao.Login
{
    public class LoginDao
    {
        public LoginDaoOutputDto GetUser(LoginDaoInputDto inputDto)
        {
            try
            {
                var dbContext = new HcsContext();
                var output = new LoginDaoOutputDto();

               
                var loginDao = from user in dbContext.Users
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
                output.UserInfoDto = loginDao.First().user;
                output.UserInfoDto.Roles = loginDao.Select(r => r.role).ToList();
               
                

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
        public bool CheckUserName(string username)
        {
            var context = new HcsContext();
            var output = context.Users.FirstOrDefault(user => user.Email == username);
            if (output != null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        public bool UpdatePassword(string username,string newPass)
        {
            var context = new HcsContext();
            PasswordGenerator passwordMD5= new PasswordGenerator();
            string newPassMD5 = passwordMD5.GetMD5Hash(newPass);
            var userToChangePass = context.Users.FirstOrDefault(user => user.Email == username);
            if (userToChangePass != null)
            {
                userToChangePass.Password = newPassMD5;
                //context.Users.Update(userToChangePass); 
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
