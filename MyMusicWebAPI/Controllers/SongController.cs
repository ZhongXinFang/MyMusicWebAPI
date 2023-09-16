using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.Tools;

namespace MyMusicWebAPI.Controllers;
[Route("api/[controller]")]
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
    public async Task<IActionResult> AddSong()
    {
        try
        {
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetSongs()
    {
        try
        {
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{songId}")]
    public async Task<IActionResult> GetSong(string songId)
    {
        try
        {
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }
}
