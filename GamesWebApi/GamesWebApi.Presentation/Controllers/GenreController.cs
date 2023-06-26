using Application.Exception;
using Application.Service.Contracts;
using Domain.Entity;
using FluentValidation;
using GamesWebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers;

[Produces("application/json")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _service;

    public GenreController(IGenreService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a genre.
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/genres
    ///     {
    ///        "name": "genre"
    ///     }
    /// </remarks>
    /// <returns>The newly created genre.</returns>
    /// <response code="201">Returns the newly created genre.</response>
    /// <response code="400">Returns all errors in the request.</response>
    /// <response code="500">Internal Server Error.</response>
    [ProducesResponseType(typeof(Genre),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), 400)]
    [ProducesResponseType(500)]
    [HttpPost(ApiRoutes.GenreRoutes.Create)]
    public async Task<IActionResult> CreateGenre([FromServices] IValidator<CreateGenreDto> validator,
        [FromBody] CreateGenreDto dto)
    {
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage);

            var errorDto = new ErrorResponseDto()
            {
                Errors = errors.ToList()
            };
            return BadRequest(errors);
        }

        try
        {
            var genre = new Genre(dto.Name);

            var genreSaved = await _service.CreateGenre(genre);

            return Created(ApiRoutes.GenreRoutes.Create, genreSaved);
        }
        catch (GenreAlreadyExistsException e)
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