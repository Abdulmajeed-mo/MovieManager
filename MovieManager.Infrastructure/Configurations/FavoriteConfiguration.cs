using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Infrastructure.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.UserId).IsRequired().HasMaxLength(450);

            builder.Property(f => f.AddedAt).IsRequired();

            builder.HasOne(f => f.Movie).WithMany(m => m.Favorites).HasForeignKey(f => f.MovieId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(f => new { f.UserId, f.MovieId }).IsUnique();
        }
    }
}