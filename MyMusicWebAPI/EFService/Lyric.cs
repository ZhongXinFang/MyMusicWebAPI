using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMusicWebAPI.EFService;
[Comment("歌词表")]
[EntityTypeConfiguration(typeof(LyricConfiguration))]
public partial class Lyric
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
    /// 创建人
    /// </summary>
    public Guid CreatebyUserId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime Createtime { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public Guid UpdatebyUserId { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime Updatetime { get; set; }

    /// <summary>
    /// 作词人（歌词翻译或者改变，此字段非歌曲的作词人，而是歌词文件的创作者）
    /// </summary>
    public Guid LyricistUserId { get; set; }

    /// <summary>
    /// 歌词的主要语言（外键）
    /// </summary>
    public Guid LanguageId { get; set; }

    public User CreatebyUser { get; set; } = null!;
    public User UpdatebyUser { get; set; } = null!;
    public User LyricistUser { get; set; } = null!;
    public Language Language { get; set; } = null!;

}
