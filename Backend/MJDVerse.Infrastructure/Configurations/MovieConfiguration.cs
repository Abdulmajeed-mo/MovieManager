using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MJDVerse.Domain.Entities;
//مسؤول عن إعداد Movie في EF Core
namespace MJDVerse.Infrastructure.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title).IsRequired().HasMaxLength(200);

            builder.Property(m => m.Description).IsRequired().HasMaxLength(2000);

            builder.Property(m => m.RuntimeMinutes).IsRequired();

            builder.Property(m => m.AverageRating).HasPrecision(3, 1);

            builder.Property(m => m.PosterUrl).HasMaxLength(500);
        }
    }
}