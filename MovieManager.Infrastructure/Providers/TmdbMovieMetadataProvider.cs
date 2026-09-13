using Microsoft.Extensions.Configuration;
using MJDVerse.Application.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MJDVerse.Infrastructure.Providers
{
    public class TmdbMovieMetadataProvider : IMovieMetadataProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TmdbMovieMetadataProvider(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var token = _configuration["Tmdb:Token"];

            _httpClient.BaseAddress = new Uri(_configuration["Tmdb:BaseUrl"]!);

            _httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<TmdbMovieDto>> GetMoviesAsync()
        {
            var response = await _httpClient.GetAsync("movie/popular?language=en-US&page=1");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TmdbMovieResponse>(json,new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

            return result?.Results ?? new List<TmdbMovieDto>();
        }


        public class TmdbMovieResponse
        {
            public List<TmdbMovieDto> Results { get; set; } = new();
        }
    }
}