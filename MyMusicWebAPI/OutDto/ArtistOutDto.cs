namespace MyMusicWebAPI.OutDto;

public class ArtistOutDto
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Fullname { get; set; } = null!;
}
