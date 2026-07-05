using MovieManager.Models;

namespace MovieManager.Services
{
    public interface IMovieService
    {
        List<Movie> GetMovies();
    }
}
