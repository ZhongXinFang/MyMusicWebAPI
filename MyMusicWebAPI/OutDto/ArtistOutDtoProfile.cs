using AutoMapper;

namespace MyMusicWebAPI.OutDto;

public class ArtistOutDtoProfile : Profile
{
    public ArtistOutDtoProfile()
    {
        CreateMap<EFService.Artist,OutDto.ArtistOutDto>();
    }
}
