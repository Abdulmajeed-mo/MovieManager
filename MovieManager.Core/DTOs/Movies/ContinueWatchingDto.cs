namespace MJDVerse.Application.DTOs.Movies
{
    public class ContinueWatchingDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public int RuntimeMinutes { get; set; }
        public decimal AverageRating { get; set; }
        public string? PosterUrl { get; set; }

        public int ProgressInSeconds { get; set; }
        public double ProgressPercentage { get; set; }
    }
}