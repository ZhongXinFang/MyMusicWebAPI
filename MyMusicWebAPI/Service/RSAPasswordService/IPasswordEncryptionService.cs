namespace MyMusicWebAPI.Service.RSAPasswordService;
public interface IPasswordEncryptionService
{
    /// <summary>
    /// 将密码加密为全局唯一哈希值
    /// </summary>
    /// <param name="password">指定要加密的密文</param>
    /// <param name="version">加密算法版本（哈希值长度可能随加密算法版本改变，但同一加密算法版本得到的哈希长度将保持一致）</param>
    /// <returns>返回加密后的密文，其中包含了加密算法的版本和其他相关信息</returns>
    public string HashPassword(string password,VersionEnum version);
    /// <summary>
    /// 验证原密码和指定的哈希是否匹配
    /// </summary>
    /// <param name="password">指定要验证的密文</param>
    /// <param name="encryptedPassword">某一密文加密后的哈希（自动解析加密算法版本）</param>
    /// <returns></returns>
    public bool VerifyPassword(string password,string encryptedPassword);
}
