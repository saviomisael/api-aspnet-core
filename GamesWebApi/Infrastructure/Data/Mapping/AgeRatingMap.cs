using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping;

public class AgeRatingMap : IEntityTypeConfiguration<AgeRating>
{
    public void Configure(EntityTypeBuilder<AgeRating> builder)
    {
        builder.ToTable("AgeRatings");
        builder.Property(x => x.Id).HasColumnName("AgeRatingId").HasMaxLength(36);
        builder.HasKey(x => x.Id).HasName("AgeRatingId");

        builder.Property(x => x.Age).HasMaxLength(3);
        builder.Property(x => x.Description).HasMaxLength(255);
    }
}