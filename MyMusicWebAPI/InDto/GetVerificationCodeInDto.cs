namespace MyMusicWebAPI.InDto;

public class GetVerificationCodeInDto
{
    ///<summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; } = null!;
    /// <summary>
    /// RSA加密后的验证码
    /// </summary>
    public CodeEnum CodeEnum { get; set; } = CodeEnum.none;
}
