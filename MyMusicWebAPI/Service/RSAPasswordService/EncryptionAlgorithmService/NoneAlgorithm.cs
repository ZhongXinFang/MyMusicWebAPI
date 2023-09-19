using System.Security.Cryptography;
using System.Text;

namespace MyMusicWebAPI.Service.RSAPasswordService.EncryptionAlgorithmService;
internal class NoneAlgorithm : IEncryptionAlgorithm
{
    private readonly static VersionEnum mVersion = VersionEnum.None;
    //生成一个随机的盐
    private string GenerateSalt()
    {
        return Guid.NewGuid().ToString("N").ToUpper();
    }

    // 加密字符串并且返回哈希字符串
    private string HashString(string str)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(str);
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2")); // 使用小写十六进制表示
            }
            string hashedInput = sb.ToString();

            return hashedInput;
        }
    }

    /// <summary>
    /// 将密码和盐混合后进行哈希(每次都会生成新盐)
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public Model HashPassword(string password)
    {
        var salt = GenerateSalt();
        var pasd = HashString(password + salt);
        var model = new Model(mVersion,pasd,salt);
        return model;
    }

    /// <summary>
    /// 比对密码和model是否匹配
    /// </summary>
    /// <param name="password"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool VerifyPassword(string password,Model model)
    {
        if (model.Version != mVersion) throw new Exception("版本不匹配");
        var pasd = HashString(password + model.Salt);
        return pasd == model.Password;
    }
}
