using Microsoft.EntityFrameworkCore;

namespace MyMusicWebAPI.EFService;

[EntityTypeConfiguration(typeof(User))]
public partial class User
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 乐观锁
    /// </summary>
    public Guid Revision { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public Guid Createby { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime Createtime { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public Guid Updateby { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime Updatetime { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 密码指纹
    /// </summary>
    public string Password { get; set; } = null!;
}
