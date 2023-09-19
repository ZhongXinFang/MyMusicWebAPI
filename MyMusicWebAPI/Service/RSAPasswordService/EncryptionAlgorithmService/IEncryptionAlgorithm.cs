namespace MyMusicWebAPI.Service.RSAPasswordService.EncryptionAlgorithmService;
internal interface IEncryptionAlgorithm
{
    public Model HashPassword(string password);
    public bool VerifyPassword(string password,Model model);
}
