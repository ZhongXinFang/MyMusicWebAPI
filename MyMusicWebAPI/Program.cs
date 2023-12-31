﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
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

// 注册 EF Core 服务
builder.Services.AddDbContext<DBContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"))
);

// 注册邮件服务
builder.Services.AddEmailSender(options =>
{
    options.Host = builder.Configuration["Email:Host"];
    options.Secret = builder.Configuration["Email:Secret"];
    options.FromAddr = builder.Configuration["Email:FromAddr"];
});

// 注册 NewtonsoftJson 服务
builder.Services.AddControllers().AddNewtonsoftJson(options =>
 {
     options.SerializerSettings.ContractResolver = new DefaultContractResolver();
     options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
     options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
 });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailCertificateCacheService,EmailCertificateCacheService>();
builder.Services.AddSingleton<IPasswordEncryptionService,PasswordEncryptionService>();
builder.Services.AddSingleton<IRSAServiceDependencyInjection,RSAPemServiceDependencyInjection>();

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

// EF Core 数据库迁移
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DBContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAny");

app.UseAuthentication();

app.UseAuthorization();

app.UseFileServer(new FileServerOptions
{
    RequestPath = "",
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")),
    EnableDefaultFiles = true,
    StaticFileOptions =
    {
        ContentTypeProvider = new FileExtensionContentTypeProvider
        {
            Mappings =
            {
                [".opus"] = "audio/ogg",
                [".lrc"] = "text/plain"
            }
        }
    }
});

app.MapControllers();

app.Run();
