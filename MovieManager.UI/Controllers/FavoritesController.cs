using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MJDVerse.Application.Interfaces;

namespace MJDVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(
            IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }






        [HttpGet]
        public async Task<IActionResult> GetMyFavorites()
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var movies =await _favoriteService.GetMyFavoritesAsync(userId);

            return Ok(movies);
        }






        [HttpPost("{movieId}")]
        public async Task<IActionResult> AddToFavorites(int movieId)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var added =await _favoriteService.AddToFavoritesAsync(userId,movieId);

            if (!added)
            {
                return BadRequest("Movie does not exist or is already in your favorites.");
            }

            return Ok("Movie added to favorites.");
        }






        [HttpDelete("{movieId}")]
        public async Task<IActionResult> RemoveFromFavorites(int movieId)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var removed =
                await _favoriteService.RemoveFromFavoritesAsync(userId,movieId);

            if (!removed)
            {
                return NotFound("Movie is not in your favorites.");
            }

            return Ok("Movie removed from favorites.");
        }
    }
}