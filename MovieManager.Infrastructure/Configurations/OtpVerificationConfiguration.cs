using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MJDVerse.Domain.Entities;

namespace MJDVerse.Infrastructure.Configurations
{
    public class OtpVerificationConfiguration : IEntityTypeConfiguration<OtpVerification>
    {
        public void Configure(EntityTypeBuilder<OtpVerification> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired().HasMaxLength(450);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(256);

            builder.Property(x => x.CodeHash).IsRequired().HasMaxLength(200);


            builder.Property(x => x.ExpiresAt)
                .IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasIndex(x => new { x.Email, x.CreatedAt });
        }
    }
}