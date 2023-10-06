using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMusicWebAPI.EFService;
[Comment("艺术家信息表")]
[EntityTypeConfiguration(typeof(ArtistConfiguration))]
public partial class Artist
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
    public Guid? UpdatebyUserId { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? Updatetime { get; set; }

    /// <summary>
    /// 名
    /// </summary>
    public string Firstname { get; set; } = null!;

    /// <summary>
    /// 姓
    /// </summary>
    public string Lastname { get; set; } = null!;

    /// <summary>
    /// 姓名（全称，不一定由 FirstName 和 LastName组成）
    /// </summary>
    public string Fullname { get; set; } = null!;

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime Dateofbirth { get; set; }

    /// <summary>
    /// 出生国家（外键）
    /// </summary>
    public Guid CountryofbirthCountryId { get; set; }

    /// <summary>
    /// 国籍（外键）
    /// </summary>
    public Guid NationalityCountryId { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public GenderEnum Gender { get; set; } = GenderEnum.none;

    /// <summary>
    /// 歌手个人描述
    /// </summary>
    public string Description { get; set; } = null!;
    public User CreatebyUser { get; set; } = null!;
    public User UpdatebyUser { get; set; } = null!;
    public Country CountryofbirthCountry { get; set; } = null!;
    public Country NationalityCountry { get; set; } = null!;
}
