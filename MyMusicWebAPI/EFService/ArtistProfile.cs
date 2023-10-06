using AutoMapper;
using MyMusicWebAPI.InDto;

namespace MyMusicWebAPI.EFService;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        CreateMap<ArtistInDto,EFService.Artist>();
    }
}