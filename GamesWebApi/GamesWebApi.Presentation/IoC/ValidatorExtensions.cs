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
        service.AddScoped<IValidator<IFormFile>, UpdateImageValidator>();
        service.AddScoped<IValidator<CreateAccountDto>, CreateAccountValidator>();
        service.AddScoped<IValidator<LoginDto>, LoginValidator>();
        service.AddScoped<IValidator<CreateReviewDto>, CreateReviewValidator>();
        service.AddScoped<IValidator<UpdateReviewDto>, UpdateReviewValidator>();
        service.AddScoped<IValidator<ChangePasswordDto>, ChangePasswordValidator>();
        service.AddScoped<IValidator<ForgotPasswordDto>, ForgotPasswordValidator>();
    }
}