using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Infrastructure.Configurations
{
    public class CrewMemberConfiguration : IEntityTypeConfiguration<CrewMember>
    {
        public void Configure(EntityTypeBuilder<CrewMember> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Department).IsRequired().HasMaxLength(100);

            builder.Property(c => c.Job).IsRequired().HasMaxLength(100);

            builder.HasOne(c => c.Movie).WithMany(m => m.CrewMembers).HasForeignKey(c => c.MovieId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Person).WithMany().HasForeignKey(c => c.PersonId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}