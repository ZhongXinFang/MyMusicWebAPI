using AutoMapper;
using MyMusicWebAPI.InDto;

namespace MyMusicWebAPI.EFService;

public class SongProfile : Profile
{
    public SongProfile()
    {
        CreateMap<AddSongInDto,EFService.Song>();
    }
}
