namespace MyMusicWebAPI.Service.EmailService;

public interface IEmailCertificateCacheService
{
    Task<string?> ObtainCodeAsync(string email,string? id = null);
    bool VerifyCache(string certificate,string email,string? id = null);
}