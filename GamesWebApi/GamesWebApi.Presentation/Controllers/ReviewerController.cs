using System.Net.Mime;
using Application.Exception;
using Domain.Entity;
using Domain.Service;
using FluentValidation;
using GamesWebApi.DTO;
using GamesWebApi.V1;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
public class ReviewerController : ControllerBase
{
    private readonly IReviewerService _service;

    public ReviewerController(IReviewerService service)
    {
        _service = service;
    }

    [HttpPost(ApiRoutes.ReviewersRoutes.CreateAccount)]
    public async Task<IActionResult> CreateAccount([FromServices] IValidator<CreateAccountDto> validator,
        CreateAccountDto dto)
    {
        var errors = await validator.ValidateAsync(dto);

        if (!errors.IsValid)
        {
            return BadRequest(new ErrorResponseDto { Errors = errors.Errors.Select(x => x.ErrorMessage).ToList() });
        }

        try
        {
            var token = await _service.CreateAccountAsync(new Reviewer { UserName = dto.UserName, Email = dto.UserName },
                dto.Password);

            return Created(ApiRoutes.ReviewersRoutes.CreateAccount, token);
        }
        catch (CreateAccountFailureException e)
        {
            return BadRequest(new ErrorResponseDto { Errors = e.Errors });
        }
    }
}