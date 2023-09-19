using System.Security.Cryptography;
using System.Text;

namespace MyMusicWebAPI.Service.PSAService;

public class RSAService : IRSAService
{
    /// <summary>
    /// 创建一对密钥
    /// </summary>
    /// <returns></returns>
    public (string publicKeyXmlStr, string privateKeyXmlStr) CreateRsaKey()
    {
        using (RSACryptoServiceProvider rsa = new(2048))
        {
            // 获取公钥和私钥
            var publicKeyXmlStr = rsa.ToXmlString(false);
            var privateKeyXmlStr = rsa.ToXmlString(true);
            return (publicKeyXmlStr, privateKeyXmlStr);
        }
    }

    /// <summary>
    /// 使用指定公钥加密数据
    /// </summary>
    /// <param name="publicKeyXmlStr"></param>
    /// <param name="rdata"></param>
    /// <returns></returns>
    public string EncryptedData(string publicKeyXmlStr,string rdata)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKeyXmlStr);
            byte[] data = Encoding.UTF8.GetBytes(rdata);
            byte[] encryptedData = rsa.Encrypt(data,false);
            string encryptedString = Convert.ToBase64String(encryptedData);

            return encryptedString;
        }
    }

    /// <summary>
    /// 使用指定私钥解密数据
    /// </summary>
    /// <param name="privateKeyXmlStr"></param>
    /// <param name="encryptedString"></param>
    /// <returns></returns>
    public string DecryptData(string privateKeyXmlStr,string encryptedString)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKeyXmlStr);
            byte[] encryptedData = Convert.FromBase64String(encryptedString);
            byte[] decryptedData = rsa.Decrypt(encryptedData,false);
            string decryptedString = Encoding.UTF8.GetString(decryptedData);
            return decryptedString;
        }
    }
}
