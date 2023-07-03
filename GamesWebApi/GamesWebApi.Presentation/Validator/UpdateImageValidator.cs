using System.Net.Mime;
using FluentValidation;
using GamesWebApi.Helpers;

namespace GamesWebApi.Validator;

public class UpdateImageValidator : AbstractValidator<IFormFile>
{
    public UpdateImageValidator()
    {
        RuleFor(x => x.Length).Must(HaveLengthLessThanOrEqualFiveMb).WithMessage("Image too big.");
        RuleFor(x => x.ContentType).Must(IsImageMediaTypeSupported).WithMessage("Image type not supported.");
    }
    
    private bool HaveLengthLessThanOrEqualFiveMb(long imageSize) =>
        ValidatorHelper.IsImageSizeLessThanOrEqualTo(imageSize, 5 * 1024 * 1024);

    private bool IsImageMediaTypeSupported(string mediaType) =>
        ValidatorHelper.IsImageTypeSupported(mediaType, new[] { MediaTypeNames.Image.Jpeg, "image/jpg", "image/png" });
}