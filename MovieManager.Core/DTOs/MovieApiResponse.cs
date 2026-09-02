using MovieManager.Domain.Entities;
namespace MovieManager.Core.DTOs
{
    public class MovieApiResponse
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
    }
}
