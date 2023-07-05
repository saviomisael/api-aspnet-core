using FluentValidation;
using GamesWebApi.DTO;

namespace GamesWebApi.Validator;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be a valid email.");
    }
}