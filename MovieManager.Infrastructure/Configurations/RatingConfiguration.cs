using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Infrastructure.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.UserId).IsRequired().HasMaxLength(450);

            builder.Property(r => r.Value).HasPrecision(3, 1);

            builder.Property(r => r.RatedAt).IsRequired();

            builder.HasOne(r => r.Movie).WithMany(m => m.Ratings).HasForeignKey(r => r.MovieId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(r => new { r.UserId, r.MovieId }).IsUnique();
        }
    }
}