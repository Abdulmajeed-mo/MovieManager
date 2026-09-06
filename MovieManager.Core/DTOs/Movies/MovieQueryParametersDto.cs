namespace MJDVerse.Application.DTOs.Movies
{
    public class MovieQueryParametersDto
    {
        public string? Query { get; set; }
        public string? Genre { get; set; }
        public string? SortBy { get; set; }
        public bool Descending { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}