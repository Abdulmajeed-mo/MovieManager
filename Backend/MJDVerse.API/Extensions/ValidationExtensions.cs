using FluentValidation;
using MJDVerse.Application.Validators.Movies;

namespace MJDVerse.API.Extensions
{
    public static class ValidationExtensions
    {
        // Contains the registration of FluentValidation validators.
         public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateMovieValidator>();

            return services;
        }
    }
}