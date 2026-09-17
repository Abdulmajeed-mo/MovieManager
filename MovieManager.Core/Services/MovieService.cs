using MJDVerse.Application.DTOs.External;
using MJDVerse.Application.DTOs.Movies;
using MJDVerse.Application.Interfaces;
using MJDVerse.Domain.Entities;
using MJDVerse.Domain.Interfaces;


namespace MJDVerse.Application.Services
{
    public class MovieService : IMovieService
    {


        private readonly IMovieRepository _movieRepository;
        private readonly IMovieMetadataProvider _movieMetadataProvider;
        private readonly IGenreRepository _genreRepository;

        public MovieService(IMovieRepository movieRepository, IMovieMetadataProvider movieMetadataProvider, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _movieMetadataProvider = movieMetadataProvider;
            _genreRepository = genreRepository;



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
                TmdbId = movie.TmdbId,
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
                TmdbId = request.TmdbId,
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
                TmdbId = movie.TmdbId,
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
                TmdbId = movie.TmdbId,
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
                TmdbId = movie.TmdbId,
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





        public async Task<List<TmdbMovieDto>> GetPopularMoviesAsync()
        {
            var movies = await _movieMetadataProvider.GetMoviesAsync();
            var genres = await _movieMetadataProvider.GetGenresAsync();

            foreach (var movie in movies)
            {
                movie.Genres = genres.Where(genre => movie.GenreIds.Contains(genre.Id)).ToList();
            }

            return movies;
        }




        public async Task<TmdbMovieDto?> GetMovieDetailsAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return null;
            }

            return await _movieMetadataProvider.GetMovieByIdAsync(movie.TmdbId);
        }



        public async Task<List<TmdbGenreDto>> GetGenresAsync()
        {
            return await _movieMetadataProvider.GetGenresAsync();
        }



        public async Task SyncGenresAsync()
        {
            var tmdbGenres = await _movieMetadataProvider.GetGenresAsync();

            foreach (var tmdbGenre in tmdbGenres)
            {
                var existingGenre = await _genreRepository.GetByIdAsync(tmdbGenre.Id);

                if (existingGenre == null)
                {
                    await _genreRepository.AddAsync(new Genre
                    {
                        Id = tmdbGenre.Id,
                        Name = tmdbGenre.Name
                    });
                }
            }

            await _genreRepository.SaveChangesAsync();
        }

    }
}