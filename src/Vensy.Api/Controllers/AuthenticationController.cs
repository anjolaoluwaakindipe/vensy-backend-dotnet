using Microsoft.AspNetCore.Mvc;
using Vensy.Contracts.Authentication;
using MediatR;
using Vensy.Application.Authentication.Commands;
using Vensy.Application.Authentication.Queries;

namespace Vensy.Api.Controllers;


[Route("api/v1/auth")]
public class AuthenticationController : ApiControllerBase
{
    private readonly IHttpContextAccessor _httpContAcc;
    private readonly ISender _sender;
    public AuthenticationController(ISender mediator, IHttpContextAccessor httpContextAccessor)
    {
        _sender = mediator;
        _httpContAcc = httpContextAccessor;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        RegisterCommand command = new(request.Email, request.Password, request.Firstname, request.Lastname, request.Username);
        var response = await _sender.Send(command);

        return response.Match(
            val => Created("", val),
        errs => Problem(errs)
        );
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginQuery query = new(request.Email, request.Password);

        var result = await _sender.Send(query);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        await _sender.Send(new UpdateUserRefreshTokenCommand(
            _httpContAcc.HttpContext?.Request.Cookies["refreshToken"],
            result.Value.RefreshToken,
            result.Value.Email));
            
        var value = result.Value;

        return Ok(new LoginResponse(
            value.Email,
            value.Firstname,
            value.Lastname,
            value.Username,
            value.AccessToken,
            value.RefreshToken));
    }

    [HttpPost("/refresh")]
    public async Task<IActionResult> Refresh()
    {
        await Task.CompletedTask;
        return Ok();
    }

}