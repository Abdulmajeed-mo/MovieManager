using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CastMember> CastMembers { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<WatchlistItem> WatchlistItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
        public DbSet<OtpVerification> OtpVerifications { get; set; }
        public DbSet<PendingRegistration> PendingRegistrations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }
    }
}