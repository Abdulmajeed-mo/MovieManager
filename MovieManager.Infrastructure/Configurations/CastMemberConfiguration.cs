using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Infrastructure.Configurations
{
    public class CastMemberConfiguration : IEntityTypeConfiguration<CastMember>
    {
        public void Configure(EntityTypeBuilder<CastMember> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CharacterName).HasMaxLength(200);

            builder.HasOne(c => c.Movie).WithMany(m => m.CastMembers).HasForeignKey(c => c.MovieId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Person).WithMany().HasForeignKey(c => c.PersonId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}