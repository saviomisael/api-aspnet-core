using System.Threading.Tasks;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GamesWebApi.Tests;

public class DatabaseFixture : IAsyncLifetime
{
    public DatabaseFixture()
    {
        Context = new AppDbContext(AppDbContextOptions.GetSqlServerOptions());
    }
    
    public AppDbContext Context { get; private set; }
    
    public async Task InitializeAsync()
    {
        Context.Genres.Add(new Genre("genre 1"));
        Context.Genres.Add(new Genre("genre 2"));
        Context.Genres.Add(new Genre("genre 3"));
        Context.Genres.Add(new Genre("genre 4"));
        Context.Genres.Add(new Genre("genre"));
        Context.Platforms.Add(new Platform("platform 1"));
        Context.Platforms.Add(new Platform("platform 2"));
        Context.Platforms.Add(new Platform("platform 3"));
        Context.Platforms.Add(new Platform("platform 4"));
        Context.Platforms.Add(new Platform("xbox"));
        await Context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await Context.Database.ExecuteSqlRawAsync("DELETE FROM GameGenre");
        await Context.Database.ExecuteSqlRawAsync("DELETE FROM GamePlatform");
        await Context.Database.ExecuteSqlRawAsync("DELETE FROM Genres");
        await Context.Database.ExecuteSqlRawAsync("DELETE FROM Platforms");
        await Context.Database.ExecuteSqlRawAsync("DELETE FROM Games");
    }
}