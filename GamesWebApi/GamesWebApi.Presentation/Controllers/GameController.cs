using System.Net.Mime;
using Application.Exception;
using Domain.Service;
using FluentValidation;
using GamesWebApi.DTO;
using GamesWebApi.Mapper;
using GamesWebApi.V1;
using Infrastructure.ImagesServerApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _service;
    private readonly IImagesServerApiClient _apiClient;

    public GameController(IGameService service, IImagesServerApiClient apiClient)
    {
        _service = service;
        _apiClient = apiClient;
    }

    [HttpPost(ApiRoutes.GameRoutes.CreateGame)]
    public async Task<IActionResult> CreateGame([FromServices] IValidator<CreateGameDto> validator, [FromForm] CreateGameDto dto)
    {
        var errors = await validator.ValidateAsync(dto);

        if (!errors.IsValid)
        {
            var errorsDto = new ErrorResponseDto()
            {
                Errors = errors.Errors.Select(x => x.ErrorMessage).ToList()
            };

            return BadRequest(errorsDto);
        }

        var image = await _apiClient.PostImageAsync(dto.Image.OpenReadStream(), dto.Image.ContentType,
            dto.Image.FileName);

        if (image is null)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new ErrorResponseDto()
            {
                Errors = new List<string>() { "Upload image service unavailable." }
            });
        }

        try
        {
            var game = await _service.CreateGameAsync(GameMapper.FromCreateGameDtoToEntity(dto, image.Url));

            return Created(ApiRoutes.GameRoutes.CreateGame, game);
        }
        catch (AgeNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = new List<string>() { e.Message } });
        }
        catch (GenreNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = new List<string>() { e.Message } });
        }
        catch (PlatformNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = new List<string>() { e.Message } });
        }
    }
}