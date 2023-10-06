using AutoMapper;
using MyMusicWebAPI.InDto;

namespace MyMusicWebAPI.EFService;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryInDto,EFService.Country>();
    }
}
