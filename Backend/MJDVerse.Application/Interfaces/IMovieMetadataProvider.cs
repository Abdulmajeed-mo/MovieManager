
using MJDVerse.Application.DTOs.External;

namespace MJDVerse.Application.Interfaces
{
    public interface IMovieMetadataProvider
    {
        Task<List<TmdbMovieDto>> GetMoviesAsync();
        Task<List<TmdbGenreDto>> GetGenresAsync();
        Task<TmdbMovieDto?> GetMovieByIdAsync(int tmdbId);
    }
}
