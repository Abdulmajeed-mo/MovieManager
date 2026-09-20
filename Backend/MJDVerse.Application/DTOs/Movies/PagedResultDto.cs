namespace MJDVerse.Application.DTOs.Movies
{
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; } = new();

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages =>  (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}