using Application.Exception;
using Application.Service.Contracts;
using Domain.Entity;
using FluentValidation;
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
    public async Task<IActionResult> CreateGenre([FromServices] IValidator<CreateGenreDto> validator,
        [FromBody] CreateGenreDto dto)
    {
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage);

            var errorDto = new ErrorResponseDto();
            errorDto.Errors = errors.ToList();
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