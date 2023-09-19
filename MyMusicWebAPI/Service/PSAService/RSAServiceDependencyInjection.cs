using System.Security.Cryptography;
using System.Text;

namespace MyMusicWebAPI.Service.PSAService;

public class RSAServiceDependencyInjection : IRSAServiceDependencyInjection
{
    public string PublicKeyXmlStr { get; }
    public string PrivateKeyXmlStr { get; }

    public RSAServiceDependencyInjection()
    {
        using RSACryptoServiceProvider rsa = new(2048);
        // 获取公钥和私钥
        PublicKeyXmlStr = rsa.ToXmlString(false);
        PrivateKeyXmlStr = rsa.ToXmlString(true);
    }

    /// <summary>
    /// 加密数据(使用私钥)
    /// </summary>
    /// <param name="rdata"></param>
    /// <returns></returns>
    public string EncryptedData(string rdata)
    {
        string? encryptedString = null!;
        try
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKeyXmlStr);
            byte[] data = Encoding.UTF8.GetBytes(rdata);
            byte[] encryptedData = rsa.Encrypt(data,false);
            encryptedString = Convert.ToBase64String(encryptedData);
        }
        catch (Exception) { }
        return encryptedString;
    }

    /// <summary>
    /// 解密数据(使用私钥)
    /// </summary>
    /// <param name="encryptedString"></param>
    /// <returns></returns>
    public string DecryptData(string encryptedString)
    {
        string? decryptedString = null!;
        try
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKeyXmlStr);
            byte[] encryptedData = Convert.FromBase64String(encryptedString);
            byte[] decryptedData = rsa.Decrypt(encryptedData,false);
            decryptedString = Encoding.UTF8.GetString(decryptedData);
            return decryptedString;
        }
        catch (Exception) { }
        return decryptedString;
    }
}
