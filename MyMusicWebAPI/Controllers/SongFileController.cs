using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMusicWebAPI.Service.FileService;
using MyMusicWebAPI.Tools;

namespace MyMusicWebAPI.Controllers;
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class SongFileController : ControllerBase
{
    // Content-Type:multipar/form-data
    [HttpPost("AddMusicFile")]
    public async Task<IActionResult> AddMusicFile()
    {
        if (HttpContext.Request.Form.Files.Count == 0)
            return BadRequest("未收到数据");
        var service = new SongFileService();
        var name = service.SaveFile(HttpContext.Request.Form.Files[0]);
        if (name is not null)
            return Ok(new { FileName = name });
        return BadRequest();
    }

    [HttpGet("{SongFileName}")]
    public IActionResult GetSongs(string SongFileName)
    {
        try
        {
            if (SongFileName.IsNullOrWhiteSpace())
                return BadRequest("未指定文件名");
            if (!System.IO.File.Exists(Path.Combine(AppPathSettings.SongFileSaveDirectory,SongFileName)))
                return BadRequest("文件不存在");
            return PhysicalFile(Path.Combine(AppPathSettings.SongFileSaveDirectory,SongFileName),"application/octet-stream",enableRangeProcessing: false);
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
