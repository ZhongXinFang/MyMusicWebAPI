namespace MyMusicWebAPI.Service.RSAPasswordService;
public class Model
{
    public Model(VersionEnum version,string password,string salt)
    {
        Version = version;
        Password = password;
        Salt = salt;
    }

    /// <summary>
    /// 版本
    /// </summary>
    public VersionEnum Version { get; set; } = VersionEnum.None;
    /// <summary>
    /// 加密后的密码
    /// </summary>
    public string Password { get; set; } = null!;
    /// <summary>
    /// 盐
    /// </summary>
    public string Salt { get; set; } = null!;
}
