using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI.EFService;
using MyMusicWebAPI.Service;
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

// Add EF Core services to the services container.
builder.Services.AddDbContext<DBContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"))
);

// Add AutoMapper services.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 暂时不启用 HTTPS.
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
