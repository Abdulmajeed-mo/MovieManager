using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MJDVerse.Application.Interfaces;

namespace MJDVerse.API.Controllers
{
    
    
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;

        public WatchlistController(
            IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        





        [HttpGet]
        public async Task<IActionResult> GetMyWatchlist()
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var movies =await _watchlistService.GetMyWatchlistAsync(userId);

            return Ok(movies);
        }









        [HttpPost("{movieId}")]
        public async Task<IActionResult> AddToWatchlist(int movieId)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var added =await _watchlistService.AddToWatchlistAsync(userId,movieId);

            if (!added)
            {
                return BadRequest("Movie does not exist or is already in your watchlist.");
            }

            return Ok("Movie added to watchlist.");
        }







        [HttpDelete("{movieId}")]
        public async Task<IActionResult> RemoveFromWatchlist(int movieId)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var removed =await _watchlistService.RemoveFromWatchlistAsync(userId,movieId);

            if (!removed)
            {
                return NotFound("Movie is not in your watchlist.");
            }

            return Ok("Movie removed from watchlist.");
        }
    }
}