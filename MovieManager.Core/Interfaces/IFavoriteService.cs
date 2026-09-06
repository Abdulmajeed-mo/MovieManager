using MJDVerse.Application.DTOs.Movies;

namespace MJDVerse.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<List<MovieDto>> GetMyFavoritesAsync(string userId);

        Task<bool> AddToFavoritesAsync(string userId,int movieId);

        Task<bool> RemoveFromFavoritesAsync(string userId,int movieId);
    }
}