namespace MJDVerse.Domain.Entities
{
    public class WatchHistory
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;

        public DateTime WatchedAt { get; set; } = DateTime.UtcNow;

        public int ProgressInSeconds { get; set; }
    }
}