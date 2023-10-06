namespace MyMusicWebAPI.OutDto;

public class AccountVerificationOutDto
{
    public string RSAPublicString { get; set; } = null!;
    public string Email { get; set; } = null!;
}
