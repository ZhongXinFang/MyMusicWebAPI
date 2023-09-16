using Ansely.Email;

namespace MyMusicWebAPI.Service;

public partial class EmailCertificateCacheService : IEmailCertificateCacheService
{
    private readonly IEmailSender mEmailSender;
    private static List<EmailCertificateCacheInfo> CacheInfos = new List<EmailCertificateCacheInfo>();

    public EmailCertificateCacheService(IEmailSender emailSender)
    {
        mEmailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
    }

    public async Task<string?> ObtainCodeAsync(string email)
    {
        string code = new Random().Next(100000,999999).ToString();
        string body = $"你的验证码是 {code} 五分钟内有效，保护账号安全，请勿转发验证码给他人";
        string title = "MyMusic 验证码";
        string name = "MyMusicServiceTest（无需回复此邮件）";
        var res = await mEmailSender.SendAsync(new EmailTemplate(name,title,body),new List<string>
            {
                email
            });
        if (res.Successed)
        {
            if (CacheInfos.Count > 0)
            {
                CacheInfos.RemoveAll(x => x.Email == email);
            }
            CacheInfos.Add(new EmailCertificateCacheInfo { Certificate = code,Email = email });
            return code;
        }
        else
        {
            return null;
            //foreach (var i in res.Errors)
            //{
            //    logger.LogError($"发送邮件阶段出现异常,邮箱:{req.Emali},{i.Description}");
            //}
        }
    }

    public bool VerifyCache(string certificate,string email)
    {
        CacheInfos = CacheInfos.Where(static x => new TimeSpan(DateTime.Now.Ticks - x.Time.Ticks).TotalMinutes < 5.0).ToList();
        if (CacheInfos.Count > 0)
        {
            var res = CacheInfos.FirstOrDefault(x => x.Certificate == certificate && x.Email == email);
            if (res is not null)
            {
                CacheInfos.Remove(res);
                return true;
            }
        }
        return false;
    }
}
