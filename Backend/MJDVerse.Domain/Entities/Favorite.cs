namespace MJDVerse.Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}