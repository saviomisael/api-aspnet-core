using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mapping;

public class ReviewMap : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        builder.Property(x => x.Id).HasMaxLength(36).HasColumnName("ReviewId");
        builder.HasKey(x => x.Id).HasName("ReviewId");

        builder.Property(x => x.Description).HasColumnType("text").IsRequired();
        builder.Property(x => x.Stars).HasColumnType("tinyint").IsRequired();
        builder.Property(x => x.CreatedAtUtcTime).IsRequired();

        builder.HasOne(x => x.Game).WithMany(x => x.Reviews).HasForeignKey(x => x.GameId);
        builder.HasOne(x => x.Reviewer).WithMany(x => x.Reviews).HasForeignKey(x => x.ReviewerId);
    }
}