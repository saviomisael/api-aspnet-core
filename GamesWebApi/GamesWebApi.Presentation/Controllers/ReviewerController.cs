using System.Net.Mime;
using Application.Exception;
using Domain.DTO;
using Domain.Entity;
using Domain.Service;
using Domain.ValueObjects;
using FluentValidation;
using GamesWebApi.DTO;
using GamesWebApi.Mapper;
using GamesWebApi.V1;
using Infrastructure.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesWebApi.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
public class ReviewerController : ControllerBase
{
    private readonly IReviewerService _service;
    private readonly TokenGenerator _tokenGenerator;

    public ReviewerController(IReviewerService service, TokenGenerator tokenGenerator)
    {
        _service = service;
        _tokenGenerator = tokenGenerator;
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
            var token = await _service.CreateAccountAsync(
                new Reviewer { UserName = dto.UserName, Email = dto.Email },
                dto.Password);

            return Created(ApiRoutes.ReviewersRoutes.CreateAccount, token);
        }
        catch (CreateAccountFailureException e)
        {
            return BadRequest(new ErrorResponseDto { Errors = e.Errors });
        }
        catch (EmailInUseException e)
        {
            return BadRequest(new ErrorResponseDto { Errors = { e.Message } });
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

    /// <summary>
    /// Delete a reviewer account.
    /// </summary>
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpDelete(ApiRoutes.ReviewersRoutes.DeleteAccount)]
    public async Task<IActionResult> DeleteAccount()
    {
        var payload = _tokenGenerator.DecodeToken(Request.Headers.Authorization[0].Split(" ")[1]);

        try
        {
            await _service.DeleteAccountAsync(payload.UserName);
            return NoContent();
        }
        catch (ReviewerNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
    }

    /// <summary>
    /// Returns a fresh token.
    /// </summary>
    /// <returns>Returns a fresh token.</returns>
    /// <response code="201">Returns a fresh token.</response>
    [ProducesResponseType(typeof(ReviewerTokenDto), StatusCodes.Status201Created)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost(ApiRoutes.ReviewersRoutes.RefreshToken)]
    public IActionResult RefreshToken()
    {
        var payload = _tokenGenerator.DecodeToken(Request.Headers.Authorization[0].Split(" ")[1]);
        var newToken = _tokenGenerator.GenerateToken(payload.Sub, payload.UserName);
        return Created(ApiRoutes.ReviewersRoutes.RefreshToken, newToken);
    }

    /// <summary>
    /// Returns information about a reviewer.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>Returns information about a reviewer.</returns>
    /// <response code="200">Returns information about a reviewer.</response>
    /// <response code="404">Reviewer not found.</response>
    [ProducesResponseType(typeof(ReviewerInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [HttpGet(ApiRoutes.ReviewersRoutes.ReviewerInfo)]
    public async Task<IActionResult> GetReviewerInfo([FromRoute] string username)
    {
        try
        {
            var info = await _service.GetReviewerInfoAsync(username);
            return Ok(info);
        }
        catch (ReviewerNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
    }

    /// <summary>
    /// Returns all games that the reviewer review.
    /// </summary>
    /// <returns>Returns all games that the reviewer review.</returns>
    /// <response code="200">Returns all games that the reviewer review.</response>
    /// <response code="404">Reviewer not found.</response>
    [ProducesResponseType(typeof(ICollection<GameResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(ApiRoutes.ReviewersRoutes.GamesByUsername)]
    public async Task<IActionResult> GetGamesByUsername()
    {
        var username = _tokenGenerator.DecodeToken(Request.Headers.Authorization[0].Split(" ")[1]).UserName;

        try
        {
            var games = await _service.GetGamesByUsernameAsync(username);
            return Ok(games.Select(GameMapper.FromEntityToGameResponseDto).ToList());
        }
        catch (ReviewerNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
    }

    /// <summary>
    /// Change password for a reviewer that already sign in.
    /// </summary>
    /// <param name="dto"></param>
    /// <response code="400">Returns the errors in the request.</response>
    /// <response code="204">Change password successfully.</response>
    /// <response code="404">Reviewer not found.</response>
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut(ApiRoutes.ReviewersRoutes.ChangePassword)]
    public async Task<IActionResult> ChangePassword([FromServices] IValidator<ChangePasswordDto> validator,
        [FromBody] ChangePasswordDto dto)
    {
        var errors = await validator.ValidateAsync(dto);

        if (!errors.IsValid)
        {
            return BadRequest(new ErrorResponseDto { Errors = errors.Errors.Select(x => x.ErrorMessage).ToList() });
        }

        var username = _tokenGenerator.DecodeToken(Request.Headers.Authorization[0].Split(" ")[1]).UserName;

        try
        {
            await _service.ChangePasswordAsync(username, dto.CurrentPassword, dto.NewPassword);
            return NoContent();
        }
        catch (ReviewerNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
        catch (ChangePasswordFailureException e)
        {
            return BadRequest(new ErrorResponseDto { Errors = e.Errors });
        }
    }

    /// <summary>
    /// Creates a temporary password.
    /// </summary>
    /// <param name="email"></param>
    /// <response code="204">Creates temporary password successfully.</response>
    /// <response code="400">Returns the errors in the request.</response>
    /// <response code="404">Reviewer not found.</response>
    /// <response code="500">Internal Server Error.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto),StatusCodes.Status500InternalServerError)]
    [HttpPost(ApiRoutes.ReviewersRoutes.ForgotPassword)]
    public async Task<IActionResult> ForgotPassword([FromServices] IValidator<ForgotPasswordDto> validator,
        string email)
    {
        var dto = new ForgotPasswordDto { Email = email };
        var errors = await validator.ValidateAsync(dto);

        if (!errors.IsValid)
        {
            return BadRequest(new ErrorResponseDto { Errors = errors.Errors.Select(x => x.ErrorMessage).ToList() });
        }

        try
        {
            await _service.CreateTemporaryPasswordAsync(dto.Email);
            return NoContent();
        }
        catch (ReviewerNotFoundException e)
        {
            return NotFound(new ErrorResponseDto { Errors = { e.Message } });
        }
        catch (InternalServerErrorException e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorResponseDto { Errors = { e.Message } });
        }
    }
}