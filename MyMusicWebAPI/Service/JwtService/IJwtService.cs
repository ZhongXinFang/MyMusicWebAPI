namespace MyMusicWebAPI.Service.JwtService;

public interface IJwtService
{
    string BuildToken(string jsonStr);
    string BuildToken(JwtSubjectModel model);
    bool ValidateToken(string token);
}