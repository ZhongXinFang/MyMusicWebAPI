namespace MyMusicWebAPI.Service.JwtService;

public static class JwtCondig
{
    private static string mIssuer = null!;
    private static string mAudience = null!;
    private static string mKey = null!;
    private static int mTime = 7 * 24 * 60 * 60;

    public static string Issuer
    {
        get
        {
            return mIssuer ?? throw new ArgumentNullException(nameof(Issuer));
        }
        set => mIssuer = value;
    }
    public static string Audience
    {
        get
        {
            return mAudience ?? throw new ArgumentNullException(nameof(Audience));
        }
        set => mAudience = value;
    }
    public static string Key
    {
        get
        {
            return mKey ?? throw new ArgumentNullException(nameof(Key));
        }
        set => mKey = value;
    }
    /// <summary>
    /// 有效时长(秒)
    /// </summary>
    public static int Time { get => mTime; set => mTime = value; }

    public static void SetJwtCondigToDI(IConfiguration configuration)
    {
        Issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentException($"{nameof(Issuer)} config is null");
        Audience = configuration["Jwt:Audience"] ?? throw new ArgumentException($"{nameof(Audience)} config is null");
        Key = configuration["Jwt:Key"] ?? throw new ArgumentException($"{nameof(Key)} config is null");
        var timeStr = configuration["Jwt:Time"] ?? throw new ArgumentException($"{nameof(Time)} config is null");
        _ = int.TryParse(timeStr,out int t);
        Time = t == 0 ? Time : t;
    }
}
