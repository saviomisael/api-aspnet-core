using System.Net.Mime;
using Application.Exception;
using Domain.DTO;
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

    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(typeof(GameResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
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

        using var memoryStream = new MemoryStream();
        await dto.Image.OpenReadStream().CopyToAsync(memoryStream);
        var image = await _apiClient.PostImageAsync(memoryStream.ToArray(), dto.Image.ContentType,
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

            return Created(ApiRoutes.GameRoutes.CreateGame, GameMapper.FromEntityToGameResponseDto(game));
        }
        catch (Exception e) when (e is AgeNotFoundException or GenreNotFoundException or PlatformNotFoundException)
        {
            await _apiClient.DeleteImageAsync(image.Name);
            return NotFound(new ErrorResponseDto { Errors = new List<string>() { e.Message } });
        }
        catch (Exception)
        {
            await _apiClient.DeleteImageAsync(image.Name);
            throw;
        }
    }
}