using System.Net.Mime;
using Application.Exception;
using Domain.DTO;
using Domain.Entity;
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

    /// <summary>
    /// Create a game.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>The newly game created.</returns>
    /// <response code="400">Returns the errors in the request.</response>
    /// <response code="503">Upload image service is unavailable.</response>
    /// <response code="201">Returns the newly game created.</response>
    /// <response code="404">Age rating / GenresNames / PlatformsNames not found.</response>
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(typeof(GameResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [HttpPost(ApiRoutes.GameRoutes.CreateGame)]
    public async Task<IActionResult> CreateGame([FromServices] IValidator<CreateGameDto> validator,
        [FromForm] CreateGameDto dto)
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

    /// <summary>
    /// Returns a game.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns a game.</returns>
    /// <response code="200">Returns a game.</response>
    /// <response code="404">Game not found.</response>
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GameResponseDto), StatusCodes.Status200OK)]
    [HttpGet(ApiRoutes.GameRoutes.GetGameById)]
    public async Task<IActionResult> GetGameById([FromRoute] string id)
    {
        try
        {
            var game = await _service.GetGameByIdAsync(id);

            return Ok(GameMapper.FromEntityToGameResponseDto(game));
        }
        catch (GameNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
    }

    /// <summary>
    /// Updates a game by id.
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="id"></param>
    /// <returns>Returns the game updated.</returns>
    /// <response code="200">Returns the game updated.</response>
    /// <response code="404">Game / AgeRating / GenresName / PlatformName not found.</response>
    [ProducesResponseType(typeof(GameResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [HttpPut(ApiRoutes.GameRoutes.UpdateGameById)]
    public async Task<IActionResult> UpdateGameById([FromServices] IValidator<UpdateGameDto> validator,
        [FromBody] UpdateGameDto dto, [FromRoute] string id)
    {
        var errors = await validator.ValidateAsync(dto);

        if (!errors.IsValid)
        {
            return BadRequest(new ErrorResponseDto { Errors = errors.Errors.Select(x => x.ErrorMessage).ToList() });
        }

        try
        {
            var gameUpdate = await _service.UpdateGameByIdAsync(GameMapper.FromUpdateGameDtoToEntity(dto, id));
            return Ok(GameMapper.FromEntityToGameResponseDto(gameUpdate));
        }
        catch (Exception e) when (e is GameNotFoundException or AgeNotFoundException or GenreNotFoundException
                                      or PlatformNotFoundException)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
    }
}