using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMusicWebAPI.EFService;
[Comment("歌曲信息表")]
[EntityTypeConfiguration(typeof(SongConfiguration))]
public partial class Song
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    /// <summary>
    /// 乐观锁
    /// </summary>
    public Guid Revision { get; set; }

    /// <summary>
    /// 创建人（外键）
    /// </summary>
    public Guid CreatebyUserId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime Createtime { get; set; }

    /// <summary>
    /// 更新人（外键）
    /// </summary>
    public Guid UpdatebyUserId { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime Updatetime { get; set; }

    /// <summary>
    /// 歌曲名称
    /// </summary>
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 歌手（外键）
    /// </summary>
    public Guid ArtistId { get; set; }

    /// <summary>
    /// 专辑
    /// </summary>
    [MaxLength(100)]
    public string Album { get; set; } = null!;

    /// <summary>
    /// 发布日期
    /// </summary>
    public DateTime Publicationdate { get; set; }

    /// <summary>
    /// 作曲家（外键）
    /// </summary>
    public Guid ComposerArtistId { get; set; }

    /// <summary>
    /// 作词家（外键）
    /// </summary>
    public Guid LyricistArtistId { get; set; }

    /// <summary>
    /// 歌曲封面【Json】
    /// </summary>
    [MaxLength(1000)]
    public string Coverimgjson { get; set; } = null!;

    /// <summary>
    /// 歌曲背景图【Json】（用于首页背景）
    /// </summary>
    [MaxLength(1000)]
    public string Backgroundimgjson { get; set; } = null!;

    /// <summary>
    /// 歌曲文件【Json】
    /// </summary>
    [MaxLength(1000)]
    public string Audiofilesjson { get; set; } = null!;

    public User CreatebyUser { get; set; } = null!;
    public User UpdatebyUser { get; set; } = null!;
    public Artist Artist { get; set; } = null!;
    public Artist ComposerArtist { get; set; } = null!;
    public Artist LyricistArtist { get; set; } = null!;
}