using Application.Exception;
using Application.Service.Contracts;
using Domain.Entity;
using FluentValidation;
using FluentValidation.Results;
using GamesWebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers;

[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _service;

    public GenreController(IGenreService service)
    {
        _service = service;
    }

    [HttpPost(ApiRoutes.GenreRoutes.Create)]
    public async Task<IActionResult> CreateGenre([FromServices] IValidator<CreateGenreDTO> validator,
        [FromBody] CreateGenreDTO dto)
    {
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        try
        {
            var genre = new Genre(dto.Name);

            var genreSaved = await _service.CreateGenre(genre);

            return Created(ApiRoutes.GenreRoutes.Create, genreSaved);
        }
        catch (GenreAlreadyExistsException e)
        {
            var errorsDto = new ErrorResponseDTO();
            errorsDto.Errors.Add(e.Message);

            return BadRequest(errorsDto);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}