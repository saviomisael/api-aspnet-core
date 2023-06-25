using GamesWebApi.IoC;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("GamesWebApi"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("GamesWebApi.Presentation");
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    });
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("cors_api", policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddInfraDependencies();
builder.Services.AddAppDependencies();
builder.Services.AddValidatorDependecies();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("cors_api");

app.MapControllers();

app.Services.RunMigrations();

app.Run();