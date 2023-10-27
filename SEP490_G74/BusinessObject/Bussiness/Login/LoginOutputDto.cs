using API.Common;

namespace HcsBE.Bussiness.Login
{
    public class LoginOutputDto : BaseOutputCommon
    {
        public UserInfo? UserInfoDto { get; set; }

        public string? Token { get; set; }
    }
    public class UserInfo
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
    }
}
