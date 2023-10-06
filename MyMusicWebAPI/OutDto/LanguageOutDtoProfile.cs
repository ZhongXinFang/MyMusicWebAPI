using AutoMapper;

namespace MyMusicWebAPI.OutDto;

public class LanguageOutDtoProfile : Profile
{
    public LanguageOutDtoProfile()
    {
        CreateMap<EFService.Language,OutDto.LanguageOutDto>();
    }
}