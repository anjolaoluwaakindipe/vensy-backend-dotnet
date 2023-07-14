using System.Reflection.Metadata;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Vensy.Application.Authentication.Common;
using Vensy.Application.Common.Error;
using Vensy.Domain.Models;

namespace Vensy.Application.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            return Error.Conflict("User.Exists", "User already Exists");
        }

        ApplicationUser newUser = new() { Firstname = request.Firstname, Lastname = request.Lastname, Email = request.Email, UserName = request.Username };
        _userManager.UserValidators.Clear();
        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            return result.Errors.Select(err => Error.Validation(err.Code, err.Description)).ToList();
        }

        return new RegisterResult("Registration Successful");

    }
}