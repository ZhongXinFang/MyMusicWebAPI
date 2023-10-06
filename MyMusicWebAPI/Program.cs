using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.Service;
using MyMusicWebAPI.Service.EmailService;
using MyMusicWebAPI.Service.JwtService;
using MyMusicWebAPI.Service.PSAService;
using MyMusicWebAPI.Service.PSAService.BouncyCastleService;
using MyMusicWebAPI.Service.RSAPasswordService;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add EmailSender services to the container.
builder.Services.AddEmailSender(options =>
{
    options.Host = builder.Configuration["Email:Host"];
    options.Secret = builder.Configuration["Email:Secret"];
    options.FromAddr = builder.Configuration["Email:FromAddr"];
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
 {
     options.SerializerSettings.ContractResolver = new DefaultContractResolver();
     options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
     options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
 });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailCertificateCacheService,EmailCertificateCacheService>();
builder.Services.AddSingleton<IPasswordEncryptionService,PasswordEncryptionService>();
builder.Services.AddSingleton<IRSAServiceDependencyInjection,RSAPemServiceDependencyInjection>();

// Add EF Core services to the services container.
builder.Services.AddDbContext<DBContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"))
);

// Add AutoMapper services.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// CORS Config
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny",builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("AuthorizationToken");
    });
});

// 添加身份验证服务
JwtCondig.SetJwtCondigToDI(builder.Configuration);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtCondig.Issuer,
        ValidAudience = JwtCondig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtCondig.Key))
    };
});
builder.Services.AddSingleton<IJwtService,JwtService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 暂时不启用 HTTPS.
//app.UseHttpsRedirection();

app.UseCors("AllowAny");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
