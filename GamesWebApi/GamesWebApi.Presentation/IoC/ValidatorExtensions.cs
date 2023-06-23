using FluentValidation;
using GamesWebApi.DTO;
using GamesWebApi.Validator;

namespace GamesWebApi.IoC;

public static class ValidatorExtensions
{
    public static void AddValidatorDependecies(this IServiceCollection service)
    {
        service.AddScoped<IValidator<CreateGenreDTO>, CreateGenreValidator>();
    }
}