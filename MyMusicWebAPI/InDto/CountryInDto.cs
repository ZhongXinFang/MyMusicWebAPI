using System.ComponentModel.DataAnnotations;

namespace MyMusicWebAPI.InDto;

public class CountryInDto
{
    /// <summary>
    /// 国家描述
    /// </summary>
    [Required]
    public string Countrydescription { get; set; } = null!;

    /// <summary>
    /// 国家代号
    /// </summary>
    [Required]
    public string Countrycode { get; set; } = null!;

}
