using Application.Exception;
using Domain.DTO;
using Domain.Entity;
using Domain.Service;
using FluentValidation;
using GamesWebApi.DTO;
using GamesWebApi.Mapper;
using GamesWebApi.V1;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers;

[Produces("application/json")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IPlatformService _platformService;

    public PlatformController(IPlatformService platformService)
    {
        _platformService = platformService;
    }

    /// <summary>
    /// Creates a platform.
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/platforms
    ///     {
    ///        "name": "platform"
    ///     }
    /// </remarks>
    /// <returns>The newly created genre.</returns>
    /// <response code="201">Returns the newly created platform.</response>
    /// <response code="400">Returns all errors in the request.</response>
    /// <response code="500">Internal Server Error.</response>
    [ProducesResponseType(typeof(PlatformResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost(ApiRoutes.PlatformRoutes.Create)]
    public async Task<IActionResult> CreatePlatform([FromServices] IValidator<CreatePlatformDto> validator,
        [FromBody] CreatePlatformDto dto)
    {
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage);

            var errorDto = new ErrorResponseDto()
            {
                Errors = errors.ToList()
            };
            return BadRequest(errorDto);
        }

        try
        {
            var platform = new Platform(dto.Name);

            var platformSaved = await _platformService.CreatePlatformAsync(platform);

            return Created(ApiRoutes.GenreRoutes.Create, PlatformMapper.FromEntityToPlatformResponseDto(platformSaved));
        }
        catch (PlatformAlreadyExistsException e)
        {
            var errorsDto = new ErrorResponseDto();
            errorsDto.Errors.Add(e.Message);

            return BadRequest(errorsDto);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Gets all platforms.
    /// </summary>
    /// <returns>Returns all platforms.</returns>
    /// <response code="200">Returns all platforms.</response>
    [ProducesResponseType(typeof(ICollection<PlatformResponseDto>), StatusCodes.Status200OK)]
    [HttpGet(ApiRoutes.PlatformRoutes.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var platforms = await _platformService.GetAllAsync();

        var platformsResponse = platforms.Select(PlatformMapper.FromEntityToPlatformResponseDto).ToList();
        return Ok(platformsResponse);
    }

    /// <summary>
    /// Delete a platform by name.
    /// </summary>
    /// <param name="name"></param>
    /// <response code="204">Deleted platform successfully.</response>
    /// <response code="404">Platform not found.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [HttpDelete(ApiRoutes.PlatformRoutes.DeleteByName)]
    public async Task<IActionResult> DeleteByName(string name)
    {
        try
        {
            await _platformService.DeleteByNameAsync(name);

            return NoContent();
        }
        catch (PlatformNotFoundException e)
        {
            var errorDto = new ErrorResponseDto();
            errorDto.Errors.Add(e.Message);

            return NotFound(errorDto);
        }
        catch (PlatformHasRelatedGamesException e)
        {
            return Conflict(new ErrorResponseDto { Errors = { e.Message } });
        }
    }
}