using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI.Dto;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.InDto;
using MyMusicWebAPI.OutDto;
using MyMusicWebAPI.Service.EmailService;
using MyMusicWebAPI.Service.JwtService;
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
    private readonly IJwtService mJwtService;

    public LoginController(DBContext dbContext,
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
                RSAPublicString = mRSAService.PublicKeyStr
            };

            return Ok(res);
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
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

            var password = mRSAService.DecryptData(loginModel.Password);
            if (password is null)
                return BadRequest("密文有误，请检测网络是否安全");

            if (!mPasswordEncryptionService.VerifyPassword(password,res.Password))
                return BadRequest("密码错误");

            Response.Headers.Add("AuthorizationToken",mJwtService.BuildToken(new JwtSubjectModel
            {
                UserId = res.Id
            }));
            Response.Headers.Add("Access-Control-Expose-Headers","AuthorizationToken");
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
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
            if (!mEmailCertificateCacheService.VerifyCache(loginModel.VerificationCode.Trim(),loginModel.Email,id: CodeEnum.Login.ToString()))
                return BadRequest("验证码无效");

            Response.Headers.Add("AuthorizationToken",mJwtService.BuildToken(new JwtSubjectModel
            {
                UserId = res.Id
            }));
            Response.Headers.Add("Access-Control-Expose-Headers","AuthorizationToken");
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
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
            if (!mEmailCertificateCacheService.VerifyCache(user.VerificationCode.Trim(),user.Email,id: CodeEnum.Register.ToString()))
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
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpPost("GetVerificationCode")]
    public async Task<IActionResult> GetVerificationCode(GetVerificationCodeInDto dto)
    {
        try
        {
            if (dto.CodeEnum == CodeEnum.none)
                return BadRequest("验证码类型错误");
            var user = await mDbContext.User.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user is null && dto.CodeEnum == CodeEnum.Login)
                return BadRequest("账号不存在");
            else if(user is not null && dto.CodeEnum == CodeEnum.Register)
                return BadRequest("账号已存在");
            var res = await mEmailCertificateCacheService.ObtainCodeAsync(dto.Email,id: dto.CodeEnum.ToString());
            if (res is null)
                return BadRequest("验证码发送失败");
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex is APInterfaceException)
                return BadRequest(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 验证 token 是否有效
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Authorize")]
    public IActionResult Authorize()
    {
        return NoContent();
    }
}
