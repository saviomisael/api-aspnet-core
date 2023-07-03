using FluentValidation;
using GamesWebApi.DTO;
using GamesWebApi.Validator;

namespace GamesWebApi.IoC;

public static class ValidatorExtensions
{
    public static void AddValidatorDependencies(this IServiceCollection service)
    {
        service.AddScoped<IValidator<CreateGenreDto>, CreateGenreValidator>();
        service.AddScoped<IValidator<CreatePlatformDto>, CreatePlatformValidator>();
        service.AddScoped<IValidator<CreateGameDto>, CreateGameValidator>();
        service.AddScoped<IValidator<UpdateGameDto>, UpdateGameValidator>();
    }
}