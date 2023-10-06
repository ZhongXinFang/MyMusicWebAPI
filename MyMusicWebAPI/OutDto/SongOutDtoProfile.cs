using AutoMapper;

namespace MyMusicWebAPI.OutDto;

public class SongOutDtoProfile : Profile
{
    public SongOutDtoProfile()
    {
        CreateMap<EFService.Song,OutDto.SongOutDto>();
    }
}