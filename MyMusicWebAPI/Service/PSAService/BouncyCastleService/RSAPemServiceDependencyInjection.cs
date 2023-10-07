using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Text;

namespace MyMusicWebAPI.Service.PSAService.BouncyCastleService;

public class RSAPemServiceDependencyInjection : IRSAServiceDependencyInjection
{
    public string PrivateKeyStr => mPrivateKeyStr ?? throw new ArgumentNullException(nameof(PrivateKeyStr));

    public string PublicKeyStr => mPublicKeyStr ?? throw new ArgumentNullException(nameof(PublicKeyStr));

    private AsymmetricCipherKeyPair mAsymmetricCipherKeyPair = null!;
    private string mPrivateKeyStr = null!;
    private string mPublicKeyStr = null!;

    public RSAPemServiceDependencyInjection()
    {
        // 创建 RSA 密钥对生成器
        RsaKeyPairGenerator rsaKeyPairGen = new RsaKeyPairGenerator();

        // 初始化密钥对生成器
        rsaKeyPairGen.Init(new KeyGenerationParameters(new SecureRandom(),2048));

        // 生成密钥对
        AsymmetricCipherKeyPair keyPair = rsaKeyPairGen.GenerateKeyPair();
        mAsymmetricCipherKeyPair = keyPair;

        // 创建 PEM 写入器，用于将密钥写入到字符串
        StringWriter stringWriter = new StringWriter();
        PemWriter pemWriter = new PemWriter(stringWriter);

        pemWriter.WriteObject(keyPair.Public);
        mPublicKeyStr = stringWriter.ToString();

        stringWriter.GetStringBuilder().Clear();
        pemWriter.WriteObject(keyPair.Private);
        mPrivateKeyStr = stringWriter.ToString();
    }

    public string DecryptData(string encryptedString)
    {
        var rsaEngine = new RsaEngine();
        // 根据接口要求，使用 PKCS#1 v1.5 填充
        var decryptEngine = new Pkcs1Encoding(rsaEngine);
        decryptEngine.Init(false,mAsymmetricCipherKeyPair.Private);
        var inputBytes = Convert.FromBase64String(encryptedString.Trim());
        var decryptedBytes = decryptEngine.ProcessBlock(inputBytes,0,inputBytes.Length);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    public string EncryptedData(string rdata)
    {
        var rsaEngine = new RsaEngine();
        // 根据接口要求，使用 PKCS#1 v1.5 填充
        var encryptEngine = new Pkcs1Encoding(rsaEngine);
        encryptEngine.Init(true,mAsymmetricCipherKeyPair.Private);
        var inputBytes = Encoding.UTF8.GetBytes(rdata.Trim());
        var encryptedBytes = encryptEngine.ProcessBlock(inputBytes,0,inputBytes.Length);
        return Convert.ToBase64String(encryptedBytes);
    }
}
