using Domain.Entity;
using Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMap());
        modelBuilder.ApplyConfiguration(new PlatformMap());
        modelBuilder.ApplyConfiguration(new AgeRatingMap());
        modelBuilder.ApplyConfiguration(new GameMap());
    }

    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<AgeRating> AgeRatings { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
}