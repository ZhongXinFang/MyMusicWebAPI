using System.ComponentModel.DataAnnotations;

namespace MyMusicWebAPI.Dto;

public class LoginInDto
{
    /// <summary>
    /// 邮箱
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 密码 (RSA or HACH)
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;
}
