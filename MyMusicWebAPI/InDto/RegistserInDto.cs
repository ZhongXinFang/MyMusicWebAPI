using System.ComponentModel.DataAnnotations;

namespace MyMusicWebAPI.Dto;

public class RegistserInDto
{
    [Required]
    [StringLength(maximumLength: 20,MinimumLength = 1)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(maximumLength: 50)]
    public string Email { get; set; } = null!;
    [Required]
    [StringLength(maximumLength: 20)]
    public string Password { get; set; } = null!;
    [Required]
    [StringLength(maximumLength: 6,MinimumLength = 6)]
    public string VerificationCode { get; set; } = null!;
}
