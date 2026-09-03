using MJDVerse.Application.Interfaces;

namespace MJDVerse.Infrastructure.Providers
{
    public class TmdbMovieMetadataProvider : IMovieMetadataProvider
    {



        private readonly HttpClient _httpClient;






        public TmdbMovieMetadataProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }







        public Task<List<TmdbMovieDto>> GetMoviesAsync()
        {
            return Task.FromResult(new List<TmdbMovieDto>());
        }
    }
}
