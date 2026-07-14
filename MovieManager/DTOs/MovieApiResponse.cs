using MovieManager.Models;

namespace MovieManager.DTOs
{
    public class MovieApiResponse
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
    }
}
