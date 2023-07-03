using System.Net.Mime;
using Application.Exception;
using Domain.DTO;
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

    /// <summary>
    /// Create a reviewer account.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>The token to be used in authorized routes.</returns>
    /// <response code="400">Returns the errors in the request.</response>
    /// <response code="201">Returns the token, expiration time and username from newly account created.</response>
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ReviewerTokenDto), StatusCodes.Status201Created)]
    [HttpPost(ApiRoutes.ReviewersRoutes.CreateAccount)]
    public async Task<IActionResult> CreateAccount([FromServices] IValidator<CreateAccountDto> validator,
        [FromBody] CreateAccountDto dto)
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

    /// <summary>
    /// Returns the token for using in authorized route.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Returns the token for using in authorized route.</returns>
    /// <response code="200">Returns the token for using in authorized route.</response>
    /// <response code="400">Returns the errors in the request.</response>
    [ProducesResponseType(typeof(ReviewerTokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [HttpPost(ApiRoutes.ReviewersRoutes.Login)]
    public async Task<IActionResult> Login([FromServices] IValidator<LoginDto> validator, [FromBody] LoginDto dto)
    {
        var errors = await validator.ValidateAsync(dto);

        if (!errors.IsValid)
        {
            return BadRequest(new ErrorResponseDto { Errors = errors.Errors.Select(x => x.ErrorMessage).ToList() });
        }

        try
        {
            var token = await _service.LoginAsync(dto.UserName, dto.Password);
            return Ok(token);
        }
        catch (LoginFailureException e)
        {
            return BadRequest(new ErrorResponseDto { Errors = { e.Message } });
        }
    }
}