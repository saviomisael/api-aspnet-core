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
using Infrastructure.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _service;
    private readonly IImagesServerApiClient _apiClient;
    private readonly TokenGenerator _tokenGenerator;

    public GameController(IGameService service, IImagesServerApiClient apiClient, TokenGenerator tokenGenerator)
    {
        _service = service;
        _apiClient = apiClient;
        _tokenGenerator = tokenGenerator;
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
    [ProducesResponseType(typeof(SingleGameResponseDto), StatusCodes.Status201Created)]
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

            return Created(ApiRoutes.GameRoutes.CreateGame, GameMapper.FromEntityToSingleGameResponseDto(game));
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
    [ProducesResponseType(typeof(SingleGameResponseDto), StatusCodes.Status200OK)]
    [HttpGet(ApiRoutes.GameRoutes.GetGameById)]
    public async Task<IActionResult> GetGameById([FromRoute] string id)
    {
        try
        {
            var game = await _service.GetGameByIdAsync(id);

            return Ok(GameMapper.FromEntityToSingleGameResponseDto(game));
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
    [ProducesResponseType(typeof(SingleGameResponseDto), StatusCodes.Status200OK)]
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
            return Ok(GameMapper.FromEntityToSingleGameResponseDto(gameUpdate));
        }
        catch (Exception e) when (e is GameNotFoundException or AgeNotFoundException or GenreNotFoundException
                                      or PlatformNotFoundException)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
    }

    /// <summary>
    /// Update image for game.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="image"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/games/{id} multipart/form-data
    ///        "image": file
    /// </remarks>
    /// <response code="204">Image updated successfully.</response>
    /// <response code="404">Game not found.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [HttpPatch(ApiRoutes.GameRoutes.UpdateImage)]
    public async Task<IActionResult> UpdateImage([FromServices] IValidator<IFormFile> validator, [FromRoute] string id,
        [FromForm] IFormFile image)
    {
        var errors = await validator.ValidateAsync(image);

        if (!errors.IsValid)
        {
            return BadRequest(new ErrorResponseDto { Errors = errors.Errors.Select(x => x.ErrorMessage).ToList() });
        }

        using var memoryStream = new MemoryStream();
        await image.OpenReadStream().CopyToAsync(memoryStream);
        var imageFromService =
            await _apiClient.PostImageAsync(memoryStream.ToArray(), image.ContentType, image.FileName);

        if (imageFromService is null)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable,
                new ErrorResponseDto { Errors = { "Upload image service is unavailable" } });
        }

        try
        {
            await _service.UpdateImageAsync(imageFromService.Url, id);
            return NoContent();
        }
        catch (GameNotFoundException e)
        {
            await _apiClient.DeleteImageAsync(imageFromService.Name);
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
    }

    /// <summary>
    /// Deletes a game by id.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="204">Game deleted successfully.</response>
    /// <response code="404">Game not found.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [HttpDelete(ApiRoutes.GameRoutes.DeleteGameById)]
    public async Task<IActionResult> DeleteGameById([FromRoute] string id)
    {
        try
        {
            await _service.DeleteGameByIdAsync(id);
            return NoContent();
        }
        catch (GameNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
    }

    /// <summary>
    /// Create a review for game.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns>The game with the new review.</returns>
    /// <response code="400">Returns the errors in the request.</response>
    /// <response code="409">The reviewer already made a review for this game.</response>
    /// <response code="404">Game / Reviewer not found.</response>
    /// <response code="201">Returns the game with the new review.</response>
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(SingleGameResponseDto), StatusCodes.Status201Created)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost(ApiRoutes.GameRoutes.AddReview)]
    public async Task<IActionResult> AddReviewToGame([FromServices] IValidator<CreateReviewDto> validator, string id,
        [FromBody] CreateReviewDto dto)
    {
        var errors = await validator.ValidateAsync(dto);

        if (!errors.IsValid)
        {
            return BadRequest(new ErrorResponseDto { Errors = errors.Errors.Select(x => x.ErrorMessage).ToList() });
        }

        var reviewerId = _tokenGenerator.DecodeToken(Request.Headers.Authorization[0].Split(" ")[1]).Sub;

        try
        {
            var game = await _service.AddReviewAsync(dto.Description, dto.Stars, id, reviewerId);
            return Created(ApiRoutes.GameRoutes.AddReview, GameMapper.FromEntityToSingleGameResponseDto(game));
        }
        catch (AlreadyReviewedGameException e)
        {
            return StatusCode(StatusCodes.Status409Conflict, new ErrorResponseDto { Errors = { e.Message } });
        }
        catch (Exception e) when (e is GameNotFoundException or ReviewerNotFoundException)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
    }

    /// <summary>
    /// Update a review and return the game with the review updated.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns>Returns a game.</returns>
    /// <response code="400">Returns the errors in the request.</response>
    /// <response code="200">Returns a game.</response>
    /// <response code="404">Review not found.</response>
    /// <response code="401">You are not the owner of the review.</response>
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SingleGameResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut(ApiRoutes.GameRoutes.UpdateReview)]
    public async Task<IActionResult> UpdateReview([FromServices] IValidator<UpdateReviewDto> validator,
        [FromRoute] string id, [FromBody] UpdateReviewDto dto)
    {
        var errors = await validator.ValidateAsync(dto);

        if (!errors.IsValid)
        {
            return BadRequest(new ErrorResponseDto { Errors = errors.Errors.Select(x => x.ErrorMessage).ToList() });
        }

        var reviewerId = _tokenGenerator.DecodeToken(Request.Headers.Authorization[0].Split(" ")[1]).Sub;

        try
        {
            var gameWithNewReview = await _service.UpdateReviewAsync(new Review
            {
                Description = dto.Description,
                Stars = dto.Stars,
                ReviewerId = reviewerId,
                Id = id
            });

            return Ok(GameMapper.FromEntityToSingleGameResponseDto(gameWithNewReview));
        }
        catch (ReviewNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
        catch (NotReviewOwnerException e)
        {
            return Unauthorized(new ErrorResponseDto { Errors = { e.Message } });
        }
    }
}