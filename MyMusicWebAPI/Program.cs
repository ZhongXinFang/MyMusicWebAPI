using Microsoft.EntityFrameworkCore;
using MyMusicWebAPI.EFService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
