using System.Security.Cryptography;
using System.Text;

namespace MyMusicWebAPI.Service.PSAService.RSACryptoServiceProviderService;

public class RSAServiceDependencyInjection : IRSAServiceDependencyInjection
{
    public string PublicKeyStr { get; }
    public string PrivateKeyStr { get; }

    public RSAServiceDependencyInjection()
    {
        using RSACryptoServiceProvider rsa = new(2048);
        // 获取公钥和私钥
        PublicKeyStr = rsa.ToXmlString(false);
        PrivateKeyStr = rsa.ToXmlString(true);
    }

    public string EncryptedData(string rdata)
    {
        string? encryptedString = null!;
        try
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKeyStr);
            byte[] data = Encoding.UTF8.GetBytes(rdata);
            byte[] encryptedData = rsa.Encrypt(data,false);
            encryptedString = Convert.ToBase64String(encryptedData);
        }
        catch (Exception) { }
        return encryptedString;
    }

    public string DecryptData(string encryptedString)
    {
        string? decryptedString = null!;
        try
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKeyStr);
            byte[] encryptedData = Convert.FromBase64String(encryptedString);
            byte[] decryptedData = rsa.Decrypt(encryptedData,false);
            decryptedString = Encoding.UTF8.GetString(decryptedData);
            return decryptedString;
        }
        catch (Exception) { }
        return decryptedString;
    }
}
