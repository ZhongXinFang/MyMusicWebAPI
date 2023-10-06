using AutoMapper;

namespace MyMusicWebAPI.OutDto;

public class CountryOutDtoPorfile : Profile
{
    public CountryOutDtoPorfile()
    {
        CreateMap<EFService.Country,OutDto.CountryOutDto>();
    }
}
