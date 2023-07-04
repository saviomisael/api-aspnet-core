using Domain.Entity;
using Infrastructure.Data.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : IdentityDbContext<Reviewer>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMap());
        modelBuilder.ApplyConfiguration(new PlatformMap());
        modelBuilder.ApplyConfiguration(new AgeRatingMap());
        modelBuilder.ApplyConfiguration(new GameMap());
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Platform> Platforms { get; set; }
    public virtual DbSet<AgeRating> AgeRatings { get; set; }
    public virtual DbSet<Game> Games { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
}