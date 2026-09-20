namespace MJDVerse.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;

        public decimal Value { get; set; }

        public DateTime RatedAt { get; set; } = DateTime.UtcNow;
    }
}