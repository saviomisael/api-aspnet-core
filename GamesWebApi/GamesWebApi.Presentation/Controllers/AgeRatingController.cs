using System.Net.Mime;
using Domain.DTO;
using Domain.Service;
using GamesWebApi.Mapper;
using GamesWebApi.V1;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
public class AgeRatingController : ControllerBase
{
    private readonly IAgeRatingService _service;

    public AgeRatingController(IAgeRatingService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns all age ratings.
    /// </summary>
    /// <returns>Returns all age ratings.</returns>
    [ProducesResponseType(typeof(ICollection<AgeRatingResponseDto>), StatusCodes.Status200OK)]
    [HttpGet(ApiRoutes.AgeRatingRoutes.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var ages = await _service.GetAllAsync();
        var agesResponse = ages.Select(AgeRatingMapper.FromEntityToAgeRatingResponseDto).ToList();
        return Ok(agesResponse);
    }
}