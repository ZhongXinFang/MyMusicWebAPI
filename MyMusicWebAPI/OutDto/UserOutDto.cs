namespace MyMusicWebAPI.OutDto;

public class UserOutDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
