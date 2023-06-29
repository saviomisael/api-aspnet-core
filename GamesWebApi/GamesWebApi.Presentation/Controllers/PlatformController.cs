using Application.Exception;
using Application.Service.Contracts;
using Domain.Entity;
using FluentValidation;
using GamesWebApi.DTO;
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
    /// <response code="201">Returns the newly created genre.</response>
    /// <response code="400">Returns all errors in the request.</response>
    /// <response code="500">Internal Server Error.</response>
    [ProducesResponseType(typeof(Platform),StatusCodes.Status201Created)]
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

            return Created(ApiRoutes.GenreRoutes.Create, platformSaved);
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
}