using MJDVerse.Domain.Enums;

namespace MJDVerse.Domain.Entities
{
    public class OtpVerification
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CodeHash { get; set; } = string.Empty;

        public OtpPurpose Purpose { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsUsed { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}