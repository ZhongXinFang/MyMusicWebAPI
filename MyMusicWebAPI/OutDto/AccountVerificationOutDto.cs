namespace MyMusicWebAPI.OutDto;

public class AccountVerificationOutDto
{
    public string RSAPublicXmlString { get; set; } = null!;
    public string Email { get; set; } = null!;
}
