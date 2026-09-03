
namespace MJDVerse.Application.Interfaces
{
    public interface IMovieMetadataProvider
    {
        Task<List<TmdbMovieDto>> GetMoviesAsync();
    }
}
