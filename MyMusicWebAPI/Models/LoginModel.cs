namespace MyMusicWebAPI.Models;

public class LoginModel
{
    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// 密码 (RSA or HACH)
    /// </summary>
    public string Password { get; set; } = null!;

}
