using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMusicWebAPI.EFService;
[Comment("国家字典")]
[EntityTypeConfiguration(typeof(CountryConfiguration))]
public partial class Country
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
    public Guid? UpdatebyUserId { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? Updatetime { get; set; }

    /// <summary>
    /// 国家描述
    /// </summary>
    public string Countrydescription { get; set; } = null!;

    /// <summary>
    /// 国家代号
    /// </summary>
    public string Countrycode { get; set; } = null!;
    public User CreatebyUser { get; set; } = null!;
    public User UpdatebyUser { get; set; } = null!;
}
