using System.Net.Mime;
using Domain.Entity;
using Domain.Service;
using ImagesServer.DTO;
using ImagesServer.V1;
using Microsoft.AspNetCore.Mvc;

namespace ImagesServer.Controllers;

[ApiController]
public class ImageController : ControllerBase
{
    private const decimal FiveMb = 5 * 1024 * 1024;
    private readonly IImageService _service;

    public ImageController(IImageService service)
    {
        _service = service;
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpPost(ApiRoutes.Images.CreateImage)]
    public async Task<IActionResult> CreateImage(IFormFile image)
    {
        var extensionIsValid = image.ContentType is MediaTypeNames.Image.Jpeg or "image/jpg" or "image/png";

        if (!extensionIsValid)
        {
            var errorsDto = new ErrorsDto();
            errorsDto.Errors.Add("Image must be jpeg, jpg, or png.");

            return BadRequest(errorsDto);
        }

        if (image.Length > FiveMb)
        {
            var errorsDto = new ErrorsDto();
            errorsDto.Errors.Add("Image must have no more than 5 MB.");
            return BadRequest(errorsDto);
        }

        var filePath = Path.GetTempFileName();
        await using var stream = System.IO.File.Create(filePath);
        await image.CopyToAsync(stream);
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);

        var imageDomain = new Image(image.ContentType.Split("/")[1], memoryStream.ToArray());

        var response = await _service.CreateImageAsync(imageDomain);
        
        return Created(ApiRoutes.Images.CreateImage, response);
    }
}