using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMusicWebAPI.EFService;
[Comment("用户表")]
[EntityTypeConfiguration(typeof(UserConfiguration))]
public partial class User
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]     // 禁用数据库自动生成
    public Guid Id { get; set; }

    /// <summary>
    /// 乐观锁
    /// </summary>
    public Guid Revision { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [ForeignKey(nameof(CreatebyUser))]
    public Guid? CreatebyUserId { get; set; }

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
    /// 用户名称
    /// </summary>
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 密码指纹
    /// </summary>
    [MaxLength(500)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    public User? CreatebyUser { get; set; }
    public User? UpdatebyUser { get; set; }
}
