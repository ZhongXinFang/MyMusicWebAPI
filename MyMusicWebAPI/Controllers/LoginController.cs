using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI.Dto;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.InDto;
using MyMusicWebAPI.OutDto;
using MyMusicWebAPI.Service.EmailService;
using MyMusicWebAPI.Service.PSAService;
using MyMusicWebAPI.Service.RSAPasswordService;
using MyMusicWebAPI.Tools;

namespace MyMusicWebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly DBContext mDbContext;
    private readonly IMapper mMapper;
    private readonly IEmailCertificateCacheService mEmailCertificateCacheService;
    private readonly IPasswordEncryptionService mPasswordEncryptionService;
    private readonly IRSAServiceDependencyInjection mRSAService;

    public LoginController(DBContext dbContext,
        IMapper mapper,
        IEmailCertificateCacheService emailCertificateCacheService,
        IPasswordEncryptionService passwordEncryptionService,
        IRSAServiceDependencyInjection rSAService)
    {
        mDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        mMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        mEmailCertificateCacheService = emailCertificateCacheService ?? throw new ArgumentNullException(nameof(emailCertificateCacheService));
        mPasswordEncryptionService = passwordEncryptionService ?? throw new ArgumentNullException(nameof(passwordEncryptionService));
        mRSAService = rSAService ?? throw new ArgumentNullException(nameof(rSAService));
    }

    /// <summary>
    /// 验证账号是否存在，并且获取RSA加密公钥
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="APInterfaceException"></exception>
    [HttpPost("AccountVerification")]
    public async Task<IActionResult> AccountVerification(AccountVerificationInDto model)
    {
        try
        {
            var user = await mDbContext.User.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user is null)
                return BadRequest("账号不存在");

            var res = new AccountVerificationOutDto
            {
                Email = model.Email,
                RSAPublicXmlString = mRSAService.PublicKeyXmlStr
            };

            return Ok(res);
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// 通过账号和密码登录
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
    /// <exception cref="APInterfaceException"></exception>
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginInDto loginModel)
    {
        try
        {
            _ = loginModel ?? throw new APInterfaceException("LoginModel is null");
            if (loginModel.Email.IsNullOrWhiteSpace())
                throw new APInterfaceException("Email is null");

            var res = await mDbContext.User.FirstOrDefaultAsync(x => x.Email == loginModel.Email);
            if (res is null)
                return BadRequest("账号不存在");

            var password = mRSAService.DecryptData(res.Password);
            if (password is null)
                return BadRequest("密文有误，请检测网络是否安全");

            if (!mPasswordEncryptionService.VerifyPassword(password,res.Password))
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

    /// <summary>
    /// 通过邮箱验证登录
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
    /// <exception cref="APInterfaceException"></exception>
    [HttpPost("LoginFromEmail")]
    public async Task<IActionResult> LoginFromEmail(LoginFromEmailInDto loginModel)
    {
        try
        {
            _ = loginModel ?? throw new APInterfaceException("LoginModel is null");
            if (loginModel.Email.IsNullOrWhiteSpace())
                throw new APInterfaceException("Email is null");
            if (loginModel.VerificationCode.IsNullOrWhiteSpace())
                throw new APInterfaceException("VerificationCode is null");

            var res = await mDbContext.User.FirstOrDefaultAsync(x => x.Email == loginModel.Email);
            if (res is null)
                return BadRequest("账号不存在");
            if (!mEmailCertificateCacheService.VerifyCache(loginModel.VerificationCode,loginModel.Email,id: CodeEnum.Login.ToString()))
                return BadRequest("验证码无效");
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// 通过邮箱验证注册
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="APInterfaceException"></exception>
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegistserInDto user)
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
            if (!mEmailCertificateCacheService.VerifyCache(user.VerificationCode,user.Email,id: CodeEnum.Register.ToString()))
                return BadRequest("验证码无效");

            var password = mRSAService.DecryptData(user.Password);
            if (password is null)
                return BadRequest("密文有误，请检测网络是否安全");
            var enPassword = mPasswordEncryptionService.HashPassword(password,VersionEnum.None);

            var userEntity = mMapper.Map<EFService.User>(user);
            userEntity.Id = Guid.NewGuid();
            userEntity.Revision = Guid.NewGuid();
            userEntity.Createtime = DateTime.Now;
            userEntity.Password = enPassword;

            await mDbContext.AddAsync(userEntity);
            await mDbContext.SaveChangesAsync();

            return Ok(mMapper.Map<UserOutDto>(userEntity));
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("GetVerificationCode")]
    public async Task<IActionResult> GetVerificationCode(string email,CodeEnum codeEnum)
    {
        try
        {
            var res = await mEmailCertificateCacheService.ObtainCodeAsync(email,id: codeEnum.ToString());
            if (res is null)
                return BadRequest("验证码发送失败");
            return Ok(new
            {
                VerificationCode = res
            });
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return NotFound(ex.Message);
        }
    }
}
