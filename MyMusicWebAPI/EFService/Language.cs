using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMusicWebAPI.EFService;
[Comment("语言字典")]
[EntityTypeConfiguration(typeof(LanguageConfiguration))]
public partial class Language
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
    /// 语言描述（简体中文，English，한국어，...）
    /// </summary>
    public string Languagedescription { get; set; } = null!;

    public User CreatebyUser { get; set; } = null!;
    public User UpdatebyUser { get; set; } = null!;
}
