using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping;

public class ImageMap : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");
        builder.Property(x => x.Id).HasMaxLength(36);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Extension).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Content).HasColumnType("varbinary(max)");
    }
}