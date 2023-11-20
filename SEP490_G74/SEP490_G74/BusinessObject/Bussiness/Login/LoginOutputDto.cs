using API.Common;
using DataAccess.Entity;

namespace HcsBE.Bussiness.Login
{
    public class LoginOutputDto : BaseOutputCommon
    {
        public UserInfo? UserInfoDto { get; set; } = new UserInfo();

        public JwtTokenOutput? KeyDto { get; set; } = new JwtTokenOutput();
    }
    public class UserInfo
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public List<Role> Roles { get; set; }
       
    }

    public class JwtTokenOutput
    {
        public string? Key { get; set;}
    }
}
