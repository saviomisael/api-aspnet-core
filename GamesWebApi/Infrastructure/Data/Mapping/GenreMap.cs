using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping;

public class GenreMap : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres");
        builder.Property(g => g.Id).HasMaxLength(36);
        builder.HasKey(g => g.Id).HasName("Id");
        builder.Property(g => g.Name).HasMaxLength(255).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}