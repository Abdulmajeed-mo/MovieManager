using System.Text.Json.Serialization;

namespace MJDVerse.Application.DTOs.External
{
    public class TmdbMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string? PosterPath { get; set; }
        public string? BackdropPath { get; set; }
        public string? ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public decimal VoteAverage { get; set; }

        public List<TmdbGenreDto> Genres { get; set; } = new();

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; } = new();
    }
}