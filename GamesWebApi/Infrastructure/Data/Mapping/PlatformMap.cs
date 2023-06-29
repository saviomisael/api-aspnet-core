using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping;

public class PlatformMap : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.ToTable("Platforms");
        builder.Property(x => x.Id).HasMaxLength(36).HasColumnName("PlatformId");
        builder.HasKey(x => x.Id).HasName("Id").HasName("PlatformId");
        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
    }
}