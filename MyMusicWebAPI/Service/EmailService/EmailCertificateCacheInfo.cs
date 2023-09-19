namespace MyMusicWebAPI.Service;

public partial class EmailCertificateCacheService
{
    public class EmailCertificateCacheInfo
    {
        public string? Id { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string Certificate { get; set; } = "000000";
        public string Email { get; set; } = string.Empty;
    }
}
