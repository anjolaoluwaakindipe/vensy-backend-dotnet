using Microsoft.AspNetCore.Mvc;
using Vensy.Contracts.Authentication;
using MediatR;
using Vensy.Application.Authentication.Commands;
using Vensy.Application.Authentication.Queries;

namespace Vensy.Api.Controllers;


[Route("api/v1/auth")]
public class AuthenticationController : ApiControllerBase
{
    private readonly ISender _sender;
    public AuthenticationController(ISender mediator)
    {
        _sender = mediator;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        RegisterCommand command = new(request.Email, request.Password, request.Firstname, request.Lastname, request.Username);
        var response = await _sender.Send(command);

        return response.Match(
            val => Ok(val),
        errs => Problem(errs)
        );
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginQuery query = new(request.Email, request.Password);

        var result = await _sender.Send(query);

        return result.Match(
        val => Ok(val),
        errs => Problem(errs)
        );
    }

}