using AutoMapper;
using MyMusicWebAPI.Dto;

namespace MyMusicWebAPI.EFService;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegistserInDto,EFService.User>();
    }
}
