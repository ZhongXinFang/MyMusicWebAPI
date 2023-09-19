﻿using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.Service;
using MyMusicWebAPI.Service.EmailService;
using MyMusicWebAPI.Service.PSAService;
using MyMusicWebAPI.Service.RSAPasswordService;
using Newtonsoft.Json;

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
     //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
     options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
     options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
 });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailCertificateCacheService,EmailCertificateCacheService>();
builder.Services.AddTransient<IPasswordEncryptionService,PasswordEncryptionService>();
builder.Services.AddTransient<IRSAServiceDependencyInjection,RSAServiceDependencyInjection>();

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
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 暂时不启用 HTTPS.
//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAny");

app.MapControllers();

app.Run();
