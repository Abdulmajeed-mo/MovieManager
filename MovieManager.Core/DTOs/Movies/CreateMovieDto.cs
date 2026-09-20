namespace MJDVerse.Application.DTOs.Movies
{
    public class CreateMovieDto
    {
        public int TmdbId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public int RuntimeMinutes { get; set; }
        public string? PosterUrl { get; set; }
    }
}