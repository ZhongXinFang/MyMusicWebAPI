using System.ComponentModel.DataAnnotations;

namespace MyMusicWebAPI.Dto;

public class LoginFromEmailInDto
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(maximumLength: 6,MinimumLength = 6)]
    public string VerificationCode { get; set; } = null!;

}
