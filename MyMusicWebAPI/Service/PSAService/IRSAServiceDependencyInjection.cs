namespace MyMusicWebAPI.Service.PSAService;

public interface IRSAServiceDependencyInjection
{
    string PrivateKeyStr { get; }
    string PublicKeyStr { get; }

    /// <summary>
    /// 解密数据(使用私钥),默认使用 PKCS#1 v1.5 填充
    /// </summary>
    /// <param name="encryptedString"></param>
    /// <returns></returns>
    string DecryptData(string encryptedString);
    /// <summary>
    /// 加密数据(使用私钥),默认使用 PKCS#1 v1.5 填充
    /// </summary>
    /// <param name="rdata"></param>
    /// <returns></returns>
    string EncryptedData(string rdata);
}