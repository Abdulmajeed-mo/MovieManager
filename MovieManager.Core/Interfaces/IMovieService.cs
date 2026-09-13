using MJDVerse.Application.DTOs.Movies;

namespace MJDVerse.Application.Interfaces
{

    //في الابستركشن. لازم تكون الفكره انك تفصل بين الـ الكنترولر والـ السيرفس. عشان لو غيرت طريقة جلب البيانات من قاعدة البيانات، ما أحتاج أغير الكود في الـ الكنترولر.

    //هذا هوالعقد.
    public interface IMovieService
    {
        Task<PagedResultDto<MovieDto>> GetMoviesAsync(MovieQueryParametersDto parameters);

        Task<MovieDto> CreateMovieAsync(CreateMovieDto request);

        Task<MovieDto?> GetMovieByIdAsync(int id);

        Task<MovieDto?> UpdateMovieAsync(int id, CreateMovieDto request);

        Task<bool> DeleteMovieAsync(int id);

        Task<List<TmdbMovieDto>> GetPopularMoviesAsync();
    }
}