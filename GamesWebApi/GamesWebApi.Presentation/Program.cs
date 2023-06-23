using GamesWebApi.IoC;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorDependecies();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("GamesWebApi"), sqpOptions =>
    {
        sqpOptions.MigrationsAssembly("GamesWebApi.Presentation");
        sqpOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    });
});
builder.Services.addInfraDependecies();

var app = builder.Build();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<AppDbContext>();
if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}

app.Run();