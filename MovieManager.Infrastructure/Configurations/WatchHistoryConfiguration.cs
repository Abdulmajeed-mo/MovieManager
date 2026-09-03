using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Infrastructure.Configurations
{
    public class WatchHistoryConfiguration : IEntityTypeConfiguration<WatchHistory>
    {
        public void Configure(EntityTypeBuilder<WatchHistory> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.UserId).IsRequired().HasMaxLength(450);

            builder.Property(w => w.WatchedAt).IsRequired();

            builder.Property(w => w.ProgressInSeconds).IsRequired();

            builder.HasOne(w => w.Movie).WithMany(m => m.WatchHistories).HasForeignKey(w => w.MovieId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(w => new { w.UserId, w.MovieId });
        }
    }
}