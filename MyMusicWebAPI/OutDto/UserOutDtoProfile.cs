using AutoMapper;

namespace MyMusicWebAPI.OutDto;

public class UserOutDtoProfile : Profile
{
    public UserOutDtoProfile()
    {
        CreateMap<EFService.User,OutDto.UserOutDto>();
    }
}
