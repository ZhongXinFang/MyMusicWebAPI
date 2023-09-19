using AutoMapper;

namespace MyMusicWebAPI.Dto;

public class RegistserInDtoProfile : Profile
{
    public RegistserInDtoProfile()
    {
        CreateMap<EFService.User,RegistserInDto>();
    }
}
