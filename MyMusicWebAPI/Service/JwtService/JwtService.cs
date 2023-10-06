using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyMusicWebAPI.Service.JwtService;

public class JwtService : IJwtService
{
    //添加 Token 验证头，被验证头标记的方法需要上传HTTP头部信息 Authorization:bearer空格+token
    /// <summary>
    /// 生成一个Token
    /// </summary>
    /// <param name="jsonStr">需要添加到Token中的Json数据</param>
    /// <returns></returns>
    public string BuildToken(string jsonStr)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(JwtCondig.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = JwtCondig.Issuer,
            Audience = JwtCondig.Audience,
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name,jsonStr) }),
            Expires = DateTime.Now.AddSeconds(JwtCondig.Time),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string BuildToken(JwtSubjectModel model)
    {
        _ = model ?? throw new ArgumentNullException(nameof(model));
        return BuildToken(JsonConvert.SerializeObject(model));
    }

    /// <summary>
    /// 手动验证 Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(JwtCondig.Key);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token,validationParameters,out _);
            return true;
        }
        catch
        {
            // Console.WriteLine("Token is invalid");
        }
        return false;
    }
}
