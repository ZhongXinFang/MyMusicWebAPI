using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.InDto;
using MyMusicWebAPI.OutDto;
using MyMusicWebAPI.Service.JwtService;
using MyMusicWebAPI.Tools;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MyMusicWebAPI.Controllers;
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class SongController : ControllerBase
{
    private readonly DBContext mDbContext;
    private readonly IMapper mMapper;

    public SongController(DBContext dbContext,IMapper mapper)
    {
        mDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        mMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpPost]
    public async Task<IActionResult> AddSong(AddSongInDto dto)
    {
        try
        {
            _ = dto ?? throw new ArgumentNullException(nameof(dto));

            var token = (User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.Name)?.Value
                ?? throw new APInterfaceException("Token 无效");
            var tokenData = JsonConvert.DeserializeObject<JwtSubjectModel>(token)
                ?? throw new APInterfaceException("Token 无效");

            var songData = mMapper.Map<EFService.Song>(dto);
            songData.Id = Guid.NewGuid();
            songData.CreatebyUserId = tokenData.UserId;
            songData.Createtime = DateTime.Now;
            songData.UpdatebyUserId = tokenData.UserId;

            var lyricData = new Lyric
            {
                SongId = songData.Id,
                Id = Guid.NewGuid(),
                CreatebyUserId = tokenData.UserId,
                LyricistUserId = tokenData.UserId,
                Createtime = DateTime.Now,
                LanguageId = mDbContext.Language.First().Id,           // 语言功能暂时没有开放，so
                Lyricfilesjson = dto.Lyricfilesjson
            };

            await mDbContext.Song.AddAsync(songData);
            await mDbContext.Lyric.AddAsync(lyricData);
            await mDbContext.SaveChangesAsync();

            var res = mMapper.Map<SongOutDto>(songData);
            res.Lyricfilesjson = lyricData.Lyricfilesjson;
            return Ok(res);
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetSongs([FromQuery] SearchInDto searchDto)
    {
        try
        {
            var queryExpression = mDbContext.Song.AsQueryable();

            if (searchDto.Q.IsNotNullOrEmpty())
            {
                queryExpression = queryExpression.Where(x => x.Title.Contains(searchDto.Q.Trim()));
            }

            var count = await queryExpression.CountAsync();

            queryExpression = queryExpression
                .OrderBy(x => x.Id)
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize);

            var res = await queryExpression.Select(x => new SongOutDto
            {
                Title = x.Title,
                Id = x.Id,
                ArtistId = x.ArtistId,
                ArtistName = x.Artist.Fullname,
                Album = x.Album,
                Publicationdate = x.Publicationdate,
                ComposerArtistId = x.ComposerArtistId,
                ComposerArtistName = x.ComposerArtist.Fullname,
                LyricistArtistId = x.LyricistArtistId,
                LyricistArtistName = x.LyricistArtist.Fullname,
                Coverimgjson = x.Coverimgjson,
                Backgroundimgjson = x.Backgroundimgjson,
                Audiofilesjson = x.Audiofilesjson,
                Lyricfilesjson = x.Lyrics.First().Lyricfilesjson         // 暂时支持一个歌词文件
            }).ToListAsync();

            if (!res.Any())
                return NoContent();
            return Ok(new SongOutListDto<SongOutDto>(
                count,
                searchDto.Page,
                (int)Math.Ceiling(count / (double)searchDto.PageSize),
                searchDto.PageSize,
                res));
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{songId}")]
    public async Task<IActionResult> GetSong(Guid songId)
    {
        try
        {
            var res = await mDbContext.Song
                .Where(x => x.Id == songId)
                .Select(x => new SongOutDto
                {
                    Title = x.Title,
                    Id = x.Id,
                    ArtistId = x.ArtistId,
                    Album = x.Album,
                    Publicationdate = x.Publicationdate,
                    ComposerArtistId = x.ComposerArtistId,
                    LyricistArtistId = x.LyricistArtistId,
                    Coverimgjson = x.Coverimgjson,
                    Backgroundimgjson = x.Backgroundimgjson,
                    Audiofilesjson = x.Audiofilesjson,
                    Lyricfilesjson = x.Lyrics.First().Lyricfilesjson         // 暂时支持一个歌词文件
                }).FirstOrDefaultAsync();
            if (res is null)
                return NoContent();
            return Ok(res);
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
