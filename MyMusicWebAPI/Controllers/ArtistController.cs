using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.InDto;
using MyMusicWebAPI.Service.EmailService;
using MyMusicWebAPI.Service.JwtService;
using MyMusicWebAPI.Service.PSAService;
using MyMusicWebAPI.Service.RSAPasswordService;
using MyMusicWebAPI.Tools;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MyMusicWebAPI.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ArtistController : ControllerBase
{
    private readonly DBContext mDbContext;
    private readonly IMapper mMapper;
    private readonly IEmailCertificateCacheService mEmailCertificateCacheService;
    private readonly IPasswordEncryptionService mPasswordEncryptionService;
    private readonly IRSAServiceDependencyInjection mRSAService;
    private readonly IJwtService mJwtService;

    public ArtistController(DBContext dbContext,
        IMapper mapper,
        IEmailCertificateCacheService emailCertificateCacheService,
        IPasswordEncryptionService passwordEncryptionService,
        IRSAServiceDependencyInjection rSAService,
        IJwtService jwtService)
    {
        mDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        mMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        mEmailCertificateCacheService = emailCertificateCacheService ?? throw new ArgumentNullException(nameof(emailCertificateCacheService));
        mPasswordEncryptionService = passwordEncryptionService ?? throw new ArgumentNullException(nameof(passwordEncryptionService));
        mRSAService = rSAService ?? throw new ArgumentNullException(nameof(rSAService));
        mJwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    }

    /// <summary>
    /// 获取所有的艺术家【暂时接口】
    /// </summary>
    /// <returns></returns>
    /// <exception cref="APInterfaceException"></exception>
    [HttpGet]
    public async Task<IActionResult> Artist()
    {
        try
        {
            var res = await mDbContext.Artist.ToListAsync();
            if (!res.Any())
                return NoContent();
            return Ok(mMapper.Map<IEnumerable<EFService.Artist>,IEnumerable<OutDto.ArtistOutDto>>(res));
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddArtist(ArtistInDto dto)
    {
        try
        {
            var token = (User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.Name)?.Value 
                ?? throw new APInterfaceException("Token 无效");
            var tokenData = JsonConvert.DeserializeObject<JwtSubjectModel>(token)
                ?? throw new APInterfaceException("Token 无效");

            var data = mMapper.Map<Artist>(dto);
            data.Id = Guid.NewGuid();
            data.CreatebyUserId = tokenData.UserId;
            data.UpdatebyUserId = tokenData.UserId;
            data.Createtime = DateTime.Now;
            data.Fullname = data.Firstname  +  data.Lastname;
            data.Description = "";
            await mDbContext.Artist.AddAsync(data);
            await mDbContext.SaveChangesAsync();
            return Ok(mMapper.Map<OutDto.ArtistOutDto>(data));
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
