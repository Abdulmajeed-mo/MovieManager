using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MJDVerse.Application.Interfaces;

namespace MJDVerse.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(
            IRatingService ratingService)
        {
            _ratingService = ratingService;
        }






        [HttpGet]
        public async Task<IActionResult> GetMyRatings()
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var movies =await _ratingService.GetMyRatedMoviesAsync(userId);

            return Ok(movies);
        }




        



        [HttpPost("{movieId}")]
        public async Task<IActionResult> RateMovie(int movieId,[FromBody] decimal value)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var rated =await _ratingService.RateMovieAsync(userId,movieId,value);

            if (!rated)
            {
                return BadRequest("Movie does not exist, rating is invalid, or movie is already rated.");
            }

            return Ok("Movie rated successfully.");
        }










        [HttpPut("{movieId}")]
        public async Task<IActionResult> UpdateRating(int movieId,[FromBody] decimal value)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var updated =await _ratingService.UpdateRatingAsync(userId,movieId,value);

            if (!updated)
            {
                return BadRequest("Rating does not exist or rating value is invalid.");
            }

            return Ok("Rating updated successfully.");
        }
    }
}