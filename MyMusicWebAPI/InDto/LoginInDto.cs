using System.ComponentModel.DataAnnotations;

namespace MyMusicWebAPI.Dto;

public class LoginInDto
{
    /// <summary>
    /// 邮箱
    /// </summary>
    [Required]
    [StringLength(maximumLength: 6,MinimumLength = 6)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 密码 (RSA or HACH)
    /// </summary>
    [Required]
    [StringLength(maximumLength: 20)]
    public string Password { get; set; } = null!;
}
