using Application.Exception;
using Domain.Entity;
using Domain.Service;
using FluentValidation;
using GamesWebApi.DTO;
using GamesWebApi.V1;
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
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return BadRequest(errorDto);
        }

        try
        {
            var genre = new Genre(dto.Name);

            var genreSaved = await _service.CreateGenreAsync(genre);

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

    /// <summary>
    /// Gets all genres.
    /// </summary>
    /// <returns>Returns all genres.</returns>
    /// <response code="200">Returns all genres.</response>
    [ProducesResponseType(typeof(ICollection<Genre>),StatusCodes.Status200OK)]
    [HttpGet(ApiRoutes.GenreRoutes.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var genres = await _service.GetAllAsync();

        return Ok(genres);
    }

    /// <summary>
    /// Deletes a genre.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Deletes a genre.</returns>
    /// <response code="204">Successfully deleted.</response>
    /// <response code="404">Genre not found.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [HttpDelete(ApiRoutes.GenreRoutes.DeleteByName)]
    public async Task<IActionResult> DeleteByName(string name)
    {
        try
        {
            await _service.DeleteByNameAsync(name);
            return NoContent();
        }
        catch (GenreNotFoundException e)
        {
            var errorsDto = new ErrorResponseDto();
            errorsDto.Errors.Add(e.Message);

            return NotFound(errorsDto);
        }
    }
}