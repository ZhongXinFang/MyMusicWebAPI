using System.ComponentModel.DataAnnotations;

namespace MyMusicWebAPI.InDto;

public class AccountVerificationInDto
{
    [Required]
    [StringLength(maximumLength: 6,MinimumLength = 6)]
    public string Email { get; set; } = null!;
}
