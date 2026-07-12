namespace MovieManager.Models
{
    public class MovieApiResponse
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
    }
}
