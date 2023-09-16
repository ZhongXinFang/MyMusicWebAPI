using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.Models;
using MyMusicWebAPI.Service;
using MyMusicWebAPI.Tools;

namespace MyMusicWebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly DBContext mDbContext;
    private readonly IMapper mMapper;
    private readonly IEmailCertificateCacheService mEmailCertificateCacheService;

    public LoginController(DBContext dbContext,IMapper mapper,IEmailCertificateCacheService emailCertificateCacheService)
    {
        mDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        mMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        mEmailCertificateCacheService = emailCertificateCacheService ?? throw new ArgumentNullException(nameof(emailCertificateCacheService));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        try
        {
            _ = loginModel ?? throw new APInterfaceException("LoginModel is null");
            if (loginModel.Email.IsNullOrWhiteSpace())
                throw new APInterfaceException("Email is null");

            var res = await mDbContext.User.FirstOrDefaultAsync(x => x.Email == loginModel.Email);
            if (res is null)
                return BadRequest("账号不存在");

            if (res.Password != loginModel.Password)
                return BadRequest("密码错误");

            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(CreateUserModel user)
    {
        try
        {
            _ = user ?? throw new APInterfaceException("User is null");
            if (user.Email.IsNullOrWhiteSpace())
                throw new APInterfaceException("Email is null");
            if (user.Password.IsNullOrWhiteSpace())
                throw new APInterfaceException("Password is null");
            if (user.Name.IsNullOrWhiteSpace())
                throw new APInterfaceException("Name is null");
            if (user.VerificationCode.IsNullOrWhiteSpace())
                throw new APInterfaceException("VerificationCode is null");

            var res = await mDbContext.User.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (res is not null)
                return BadRequest("账号已存在");
            if (!mEmailCertificateCacheService.VerifyCache(user.VerificationCode,user.Email))
                return BadRequest("验证码无效");

            var userEntity = mMapper.Map<EFService.User>(user);
            userEntity.Id = Guid.NewGuid();
            userEntity.Revision = Guid.NewGuid();
            userEntity.Createtime = DateTime.Now;

            await mDbContext.AddAsync(userEntity);
            await mDbContext.SaveChangesAsync();

            return Ok(mMapper.Map<Models.CreateUserModel>(userEntity));
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }

    [HttpGet("GetVerificationCode")]
    public async Task<IActionResult> GetVerificationCode(string email)
    {
        try
        {
            var res = await mEmailCertificateCacheService.ObtainCodeAsync(email);
            if (res is null)
                return BadRequest("验证码发送失败");
            return Ok(res);
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }
}
