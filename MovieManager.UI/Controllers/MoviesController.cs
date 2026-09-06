using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MJDVerse.Application.DTOs.Movies;
using MJDVerse.Application.Interfaces;

namespace MJDVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IValidator<CreateMovieDto> _validator;








        public MoviesController(IMovieService movieService,IValidator<CreateMovieDto> validator)
        {
            _movieService = movieService;
            _validator = validator;
        }











        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieDto request)
        {
            var validationResult =await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var movie =await _movieService.CreateMovieAsync(request);

            return Ok(movie);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            return Ok(movie);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id ,CreateMovieDto request)
        {
            var validationResult =await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var movie =await _movieService.UpdateMovieAsync(id, request);

            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            return Ok(movie);
        }








        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deleted = await _movieService.DeleteMovieAsync(id);

            if (!deleted)
            {
                return NotFound("Movie not found.");
            }

            return Ok("Movie deleted successfully.");
        }









        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] MovieQueryParametersDto parameters)
        {
            var movies = await _movieService.GetMoviesAsync(parameters);

            return Ok(movies);
        }





       
    }
}