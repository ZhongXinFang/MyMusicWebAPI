namespace MyMusicWebAPI.OutDto;

public class SongOutDto
{
    public Guid Id { get; set; }
    /// <summary>
    /// 歌曲名称
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// 歌手
    /// </summary>
    public Guid ArtistId { get; set; }
    public string ArtistName { get; set; } = null!;

    /// <summary>
    /// 专辑
    /// </summary>
    public string Album { get; set; } = null!;

    /// <summary>
    /// 发布日期
    /// </summary>
    public DateTime Publicationdate { get; set; }

    /// <summary>
    /// 作曲家
    /// </summary>
    public Guid ComposerArtistId { get; set; }
    public string ComposerArtistName { get; set; } = null!;

    /// <summary>
    /// 作词家
    /// </summary>
    public Guid LyricistArtistId { get; set; }
    public string LyricistArtistName { get; set; } = null!;

    /// <summary>
    /// 歌曲封面【Json】
    /// </summary>
    public string Coverimgjson { get; set; } = null!;

    /// <summary>
    /// 歌曲背景图【Json】（用于首页背景）
    /// </summary>
    public string Backgroundimgjson { get; set; } = null!;

    /// <summary>
    /// 歌曲文件【Json】
    /// </summary>
    public string Audiofilesjson { get; set; } = null!;
    /// <summary>
    /// 歌词文件【Json】
    /// </summary>
    public string Lyricfilesjson { get; set; } = null!;

}
