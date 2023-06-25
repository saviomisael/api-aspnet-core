using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamesWebApi.IoC;

public static class MigrationExtensions
{
    public static void RunMigrations(this IServiceProvider service)
    {
        using var scope = service.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<AppDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}