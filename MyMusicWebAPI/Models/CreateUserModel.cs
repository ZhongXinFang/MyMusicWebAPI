namespace MyMusicWebAPI.Models;

public class CreateUserModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    /// <summary>
    /// 验证码
    /// </summary>
    public string VerificationCode { get; set; } = null!;
}
