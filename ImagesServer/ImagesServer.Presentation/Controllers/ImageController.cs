using System.Net;
using System.Net.Mime;
using Domain.DTO;
using Domain.Entity;
using Domain.Repository;
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
    private readonly IImageRepository _repository;

    public ImageController(IImageService service, IImageRepository repository)
    {
        _service = service;
        _repository = repository;
    }

    /// <summary>
    /// Save an image.
    /// </summary>
    /// <param name="image"></param>
    /// <returns>The url of image saved.</returns>
    /// <response code="415">Server only support images of type jpeg, jpg, and png.</response>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ErrorsDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(ImageResponseDto), StatusCodes.Status201Created)]
    [HttpPost(ApiRoutes.Images.CreateImage)]
    public async Task<IActionResult> CreateImage(IFormFile image)
    {
        var extensionIsValid = image.ContentType is MediaTypeNames.Image.Jpeg or "image/jpg" or "image/png";

        if (!extensionIsValid)
        {
            return new UnsupportedMediaTypeResult();
        }

        if (image.Length > FiveMb)
        {
            var errorsDto = new ErrorsDto();
            errorsDto.Errors.Add("Image must have no more than 5 MB.");
            return BadRequest(errorsDto);
        }
        
        using var memoryStream = new MemoryStream();
        await image.OpenReadStream().CopyToAsync(memoryStream);

        var imageDomain = new Image(image.ContentType.Split("/")[1], memoryStream.ToArray());

        var response = await _service.CreateImageAsync(imageDomain);

        return Created(ApiRoutes.Images.CreateImage, response);
    }

    /// <summary>
    /// Return an image from database.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Return an image from database.</returns>
    /// <response code="404">Image not found.</response>
    /// <response code="200">Return image requested.</response>
    [Produces(MediaTypeNames.Image.Jpeg, "image/jpg", "image/png", MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ErrorsDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [HttpGet(ApiRoutes.Images.GetImage)]
    public async Task<IActionResult> GetImage(string name)
    {
        var image = await _repository.GetImageAsync(name);
        
        if (image is null)
        {
            var errorDto = new ErrorsDto();
            errorDto.Errors.Add("Image not found.");
            return NotFound(errorDto);
        }

        return new FileContentResult(image.Content, $"image/{image.Extension}");
    }
}