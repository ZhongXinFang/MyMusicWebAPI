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
public class CountryController : ControllerBase
{
    private readonly DBContext mDbContext;
    private readonly IMapper mMapper;
    private readonly IEmailCertificateCacheService mEmailCertificateCacheService;
    private readonly IPasswordEncryptionService mPasswordEncryptionService;
    private readonly IRSAServiceDependencyInjection mRSAService;
    private readonly IJwtService mJwtService;

    public CountryController(DBContext dbContext,
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

    [HttpGet]
    public async Task<IActionResult> Country()
    {
        try
        {
            var res = await mDbContext.Country.ToListAsync();
            if (!res.Any())
                return NoContent();
            return Ok(mMapper.Map<IEnumerable<EFService.Country>,IEnumerable<OutDto.CountryOutDto>>(res));
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCountry(CountryInDto dto)
    {
        try
        {
            var token = (User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.Name)?.Value 
                ?? throw new APInterfaceException("Token 无效");
            var tokenData = JsonConvert.DeserializeObject<JwtSubjectModel>(token)
                ?? throw new APInterfaceException("Token 无效");

            var data = mMapper.Map<Country>(dto);
            data.Id = Guid.NewGuid();
            data.CreatebyUserId = tokenData.UserId;
            data.UpdatebyUserId = tokenData.UserId;
            data.Createtime = DateTime.Now;
            
            await mDbContext.Country.AddAsync(data);
            await mDbContext.SaveChangesAsync();
            return Ok(mMapper.Map<OutDto.CountryOutDto>(data));
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
