using System.ComponentModel.DataAnnotations;

namespace MyMusicWebAPI.InDto;

public class LanguageInDto
{
    [Required]
    public string Languagedescription { get; set; } = null!;
}
