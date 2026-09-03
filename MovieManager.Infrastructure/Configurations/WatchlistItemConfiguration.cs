using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Infrastructure.Configurations
{
    public class WatchlistItemConfiguration : IEntityTypeConfiguration<WatchlistItem>
    {
        public void Configure(EntityTypeBuilder<WatchlistItem> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.UserId).IsRequired().HasMaxLength(450);

            builder.Property(w => w.AddedAt).IsRequired();

            builder.HasOne(w => w.Movie).WithMany(m => m.WatchlistItems).HasForeignKey(w => w.MovieId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(w => new { w.UserId, w.MovieId }).IsUnique();
        }
    }
}