using MJDVerse.Domain.Entities;




namespace MJDVerse.Domain.Interfaces
{
    public interface IMovieRepository
    {
        //لأننا نخبر أي كلاس ينفذ هذا الـ الانترفيس بأنه يجب أن يوفر دالة اسمها قت الل موفي 


        Task<(List<Movie> Movies, int TotalCount)> GetMoviesAsync(string? query,string? genre,string? sortBy,bool descending,int page,int pageSize); Task AddAsync(Movie movie);
        Task<Movie?> GetByIdAsync(int id);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);
    }
}