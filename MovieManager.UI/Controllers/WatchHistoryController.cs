using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MJDVerse.Application.Interfaces;

namespace MJDVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WatchHistoryController : ControllerBase
    {

        private readonly IWatchHistoryService _watchHistoryService;




        public WatchHistoryController(
            IWatchHistoryService watchHistoryService)
        {
            _watchHistoryService = watchHistoryService;
        }







        [HttpGet]
        public async Task<IActionResult> GetMyWatchHistory()
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var history =await _watchHistoryService.GetMyWatchHistoryAsync(userId);

            return Ok(history);
        }








        [HttpPost("{movieId}")]
        public async Task<IActionResult> AddOrUpdateWatchHistory(int movieId, [FromBody] int progressInSeconds)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _watchHistoryService.AddOrUpdateWatchHistoryAsync(userId, movieId, progressInSeconds);

            if (!result)
            {
                return BadRequest("Movie does not exist or progress value is invalid.");
            }

            return Ok("Watch history updated successfully.");
        }













            [HttpGet("continue")]
            public async Task<IActionResult> GetContinueWatching()
            {
                var userId =
                    User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    return Unauthorized();
                }

                var movies =await _watchHistoryService.GetContinueWatchingAsync(userId);

                return Ok(movies);
            
            }


    }
}