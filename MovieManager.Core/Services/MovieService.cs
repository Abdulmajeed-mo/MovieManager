using MJDVerse.Application.DTOs.Movies;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;


namespace MJDVerse.Application.Services
{
    public class MovieService : IMovieService
    {


        private readonly IMovieRepository _movieRepository;
        


        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }






     




        public async Task<PagedResultDto<MovieDto>> GetMoviesAsync(
      MovieQueryParametersDto parameters)
        {
            var result = await _movieRepository.GetMoviesAsync(
                parameters.Query,
                parameters.Genre,
                parameters.SortBy,
                parameters.Descending,
                parameters.Page,
                parameters.PageSize);

            var movieDtos = result.Movies.Select(movie => new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                RuntimeMinutes = movie.RuntimeMinutes,
                AverageRating = movie.AverageRating,
                PosterUrl = movie.PosterUrl
            }).ToList();

            return new PagedResultDto<MovieDto>
            {
                Items = movieDtos,
                Page = parameters.Page,
                PageSize = parameters.PageSize,
                TotalCount = result.TotalCount
            };
        }








        public async Task<MovieDto> CreateMovieAsync(CreateMovieDto request)
        {
            var movie = new Movie
            {
                Title = request.Title,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
                RuntimeMinutes = request.RuntimeMinutes,
                PosterUrl = request.PosterUrl
            };

            await _movieRepository.AddAsync(movie);

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                RuntimeMinutes = movie.RuntimeMinutes,
                AverageRating = movie.AverageRating,
                PosterUrl = movie.PosterUrl
            };
        }







        public async Task<MovieDto?> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return null;
            }

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                RuntimeMinutes = movie.RuntimeMinutes,
                AverageRating = movie.AverageRating,
                PosterUrl = movie.PosterUrl
            };
        }






        public async Task<MovieDto?> UpdateMovieAsync(int id,CreateMovieDto request)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return null;
            }

            movie.Title = request.Title;
            movie.Description = request.Description;
            movie.ReleaseDate = request.ReleaseDate;
            movie.RuntimeMinutes = request.RuntimeMinutes;
            movie.PosterUrl = request.PosterUrl;

            await _movieRepository.UpdateAsync(movie);

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                RuntimeMinutes = movie.RuntimeMinutes,
                AverageRating = movie.AverageRating,
                PosterUrl = movie.PosterUrl
            };
        }







        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return false;
            }

            await _movieRepository.DeleteAsync(id);

            return true;
        }




    




    }
}