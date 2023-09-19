namespace MyMusicWebAPI.Service.PSAService;

public interface IRSAServiceDependencyInjection
{
    string PrivateKeyXmlStr { get; }
    string PublicKeyXmlStr { get; }

    /// <summary>
    /// 解密数据(使用私钥)
    /// </summary>
    /// <param name="encryptedString"></param>
    /// <returns></returns>
    string DecryptData(string encryptedString);
    /// <summary>
    /// 加密数据(使用私钥)
    /// </summary>
    /// <param name="rdata"></param>
    /// <returns></returns>
    string EncryptedData(string rdata);
}