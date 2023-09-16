using AutoMapper;

namespace MyMusicWebAPI.Models;

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<EFService.User,Models.CreateUserModel>();
    }
}
