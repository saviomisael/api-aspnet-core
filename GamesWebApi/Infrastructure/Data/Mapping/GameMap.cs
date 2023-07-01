using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping;

public class GameMap : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");
        builder.Property(x => x.Id).HasMaxLength(36).HasColumnName("GameId");
        builder.HasKey(x => x.Id).HasName("GameId");

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.UrlImage).IsRequired();
        builder.Property(x => x.Description).IsRequired().HasColumnType("text");
        builder.Property(x => x.Price).IsRequired().HasColumnType("numeric(10,2)");
        builder.Property(x => x.ReleaseDate).IsRequired().HasColumnType("datetime2");
        builder
            .HasOne(x => x.AgeRating)
            .WithMany(x => x.Games)
            .HasForeignKey(x => x.AgeRatingId).OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.Genres)
            .WithMany(x => x.Games)
            .UsingEntity<GameGenre>(r => r.HasOne<Genre>().WithMany().HasForeignKey(y => y.GenresId),
                l => l.HasOne<Game>().WithMany().HasForeignKey(x => x.GamesId)
            );
        
        builder
            .HasMany(x => x.Platforms)
            .WithMany(x => x.Games)
            .UsingEntity<GamePlatform>(r => r.HasOne<Platform>().WithMany().HasForeignKey(y => y.PlatformsId),
                l => l.HasOne<Game>().WithMany().HasForeignKey(x => x.GamesId).HasConstraintName("GamesFK")
            );
    }
}