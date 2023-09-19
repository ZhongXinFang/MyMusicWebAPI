namespace MyMusicWebAPI.Service.PSAService;

public interface IRSAService
{
    (string publicKeyXmlStr, string privateKeyXmlStr) CreateRsaKey();
    string DecryptData(string privateKeyXmlStr,string encryptedString);
    string EncryptedData(string publicKeyXmlStr,string rdata);
}