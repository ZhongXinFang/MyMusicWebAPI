using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.InDto;
using MyMusicWebAPI.Service.JwtService;
using MyMusicWebAPI.Tools;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MyMusicWebAPI.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LanguageController : ControllerBase
{
    private readonly DBContext mDbContext;
    private readonly IMapper mMapper;

    public LanguageController(DBContext dbContext,
        IMapper mapper)
    {
        mDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        mMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> Language()
    {
        try
        {
            var res = await mDbContext.Language.ToListAsync();
            if (!res.Any())
                return NoContent();
            return Ok(mMapper.Map<IEnumerable<EFService.Language>,IEnumerable<OutDto.LanguageOutDto>>(res));
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddLanguage(LanguageInDto dto)
    {
        try
        {
            var token = (User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.Name)?.Value
                ?? throw new APInterfaceException("Token 无效");
            var tokenData = JsonConvert.DeserializeObject<JwtSubjectModel>(token)
                ?? throw new APInterfaceException("Token 无效");

            var data = mMapper.Map<Language>(dto);
            data.Id = Guid.NewGuid();
            data.CreatebyUserId = tokenData.UserId;
            data.UpdatebyUserId = tokenData.UserId;
            data.Createtime = DateTime.Now;

            await mDbContext.Language.AddAsync(data);
            await mDbContext.SaveChangesAsync();
            return Ok(mMapper.Map<OutDto.LanguageOutDto>(data));
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
