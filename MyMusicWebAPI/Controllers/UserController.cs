using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.Models;
using MyMusicWebAPI.Tools;

namespace MyMusicWebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DBContext mDbContext;
    public UserController(DBContext dbContext)
    {
        mDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    [HttpGet]
    public IActionResult CreateUser(UserModel user)
    {
        try
        {
            _ = user ?? throw new APInterfaceException("User is null");
            //var userEntity = new EFService.User
            //{
            //    Id = new Guid;
            //}


        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }
}
