using FluentValidation;
using MJDVerse.Application.DTOs.Movies;

namespace MJDVerse.Application.Validators.Movies
{
    public class CreateMovieValidator : AbstractValidator<CreateMovieDto>
    {
        public CreateMovieValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);

            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);

            RuleFor(x => x.ReleaseDate).NotEmpty();

            RuleFor(x => x.RuntimeMinutes).GreaterThan(0);

            RuleFor(x => x.PosterUrl).MaximumLength(500).When(x => !string.IsNullOrWhiteSpace(x.PosterUrl));
        }
    }
}