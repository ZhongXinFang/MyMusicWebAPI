using AutoMapper;
using MyMusicWebAPI.InDto;

namespace MyMusicWebAPI.EFService;

public class LanguageInDtoProfile : Profile
{
    public LanguageInDtoProfile()
    {
        CreateMap<LanguageInDto,EFService.Language>();
    }
}
