namespace MyMusicWebAPI.Service;

public interface IEmailCertificateCacheService
{
    Task<string?> ObtainCodeAsync(string email);
    bool VerifyCache(string certificate,string email);
}