using System.Net;
using System.Net.Mime;
using Domain.Entity;
using Domain.Service;
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

    [ProducesResponseType(typeof(ICollection<AgeRating>), StatusCodes.Status200OK)]
    [HttpGet(ApiRoutes.AgeRatingRoutes.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var ages = await _service.GetAllAsync();
        return Ok(ages);
    }
}