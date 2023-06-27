using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamesWebApi.Tests;

public static class AppDbContextOptions
{
    public static DbContextOptions<AppDbContext> GetInMemoryOptions()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("database")
            .Options;
    }

    public static DbContextOptions<AppDbContext> GetSqlServerOptions()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(
                "Data Source=api_db; Initial Catalog=gamesdb; User Id=SA; Password=123aBc@#;TrustServerCertificate=True;Encrypt=False;")
            .Options;
    }
}