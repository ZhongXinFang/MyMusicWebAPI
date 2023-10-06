using System.ComponentModel.DataAnnotations;

namespace MyMusicWebAPI.InDto;

public class AddSongInDto
{
    /// <summary>
    /// 歌曲名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 歌手
    /// </summary>
    [Required]
    public Guid ArtistId { get; set; }

    /// <summary>
    /// 专辑
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Album { get; set; } = null!;

    /// <summary>
    /// 发布日期
    /// </summary>
    [Required]
    public DateTime Publicationdate { get; set; }

    /// <summary>
    /// 作曲家
    /// </summary>
    [Required]
    public Guid ComposerArtistId { get; set; }

    /// <summary>
    /// 作词家
    /// </summary>
    [Required]
    public Guid LyricistArtistId { get; set; }

    /// <summary>
    /// 歌曲封面【Json】
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Coverimgjson { get; set; } = null!;

    /// <summary>
    /// 歌曲背景图【Json】（用于首页背景）
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Backgroundimgjson { get; set; } = null!;

    /// <summary>
    /// 歌曲文件【Json】
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Audiofilesjson { get; set; } = null!;

    /// <summary>
    /// 歌词文件【Json】
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Lyricfilesjson { get; set; } = null!;

}
