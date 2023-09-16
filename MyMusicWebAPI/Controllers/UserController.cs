using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.Service;

namespace MyMusicWebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DBContext mDbContext;
    private readonly IMapper mMapper;

    public UserController(DBContext dbContext,IMapper mapper,IEmailCertificateCacheService emailCertificateCacheService)
    {
        mDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        mMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
}
