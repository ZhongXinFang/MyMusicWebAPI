using MyMusicWebAPI.Service.RSAPasswordService.EncryptionAlgorithmService;
using System.Text;
using System.Text.Json;

namespace MyMusicWebAPI.Service.RSAPasswordService;

public class PasswordEncryptionService : IPasswordEncryptionService
{
    private Model mModel = null!;
    private string SerializeModel(Model model)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model)));
    }

    private Model DeserializeModel(string base64)
    {
        return JsonSerializer.Deserialize<Model>(Encoding.UTF8.GetString(Convert.FromBase64String(base64)))!;
    }

    public string HashPassword(string password,VersionEnum version)
    {
        if (version == VersionEnum.None)
        {
            IEncryptionAlgorithm algorithm = new NoneAlgorithm();
            mModel = algorithm.HashPassword(password);
            return SerializeModel(mModel);
        }
        throw new NotImplementedException($"{nameof(version)} 不受支持");
    }

    public bool VerifyPassword(string password,string encryptedPassword)
    {
        var model = DeserializeModel(encryptedPassword);
        if (model.Version == VersionEnum.None)
        {
            IEncryptionAlgorithm algorithm = new NoneAlgorithm();
            return algorithm.VerifyPassword(password,model);
        }
        throw new NotImplementedException();
    }

}
