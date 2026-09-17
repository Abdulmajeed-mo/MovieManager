using Microsoft.Extensions.Configuration;
using MJDVerse.Application.DTOs.External;
using MJDVerse.Application.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MJDVerse.Infrastructure.Providers
{
    public class TmdbMovieMetadataProvider : IMovieMetadataProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TmdbMovieMetadataProvider(HttpClient httpClient,IConfiguration configuration)
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




        public async Task<List<TmdbGenreDto>> GetGenresAsync()
        {
            var response = await _httpClient.GetAsync("genre/movie/list?language=en-US");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TmdbGenreResponse>(json,new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result?.Genres ?? new List<TmdbGenreDto>();
        }


        public async Task<TmdbMovieDto?> GetMovieByIdAsync(int tmdbId)
        {
            var response = await _httpClient.GetAsync(
                $"movie/{tmdbId}?language=en-US");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TmdbMovieDto>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }



        public class TmdbGenreResponse
        {
            public List<TmdbGenreDto> Genres { get; set; } = new();
        }




        public class TmdbMovieResponse
        {
            public List<TmdbMovieDto> Results { get; set; } = new();
        }
    }
}