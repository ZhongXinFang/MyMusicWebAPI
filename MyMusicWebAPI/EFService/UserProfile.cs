using AutoMapper;

namespace MyMusicWebAPI.EFService;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Models.CreateUserModel,EFService.User>();
    }
}
