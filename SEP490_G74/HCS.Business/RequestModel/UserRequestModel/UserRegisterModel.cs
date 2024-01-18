namespace HCS.Business.RequestModel.UserRequestModel;

public class UserRegisterModel
{
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Status { get; set; }

    public int RoleId { get; set; }

    public int? CategoryId { get; set; } = null;
}

public class AccountUpdateModel
{
    public int UserId { get; set; }
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;

    public int RoleId { get; set; }

    public int? CategoryId { get; set; } = null;
}