using System.Net;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Vensy.Application.Authentication.Common;
using Vensy.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Vensy.Application.Common.Interfaces.Services;
using Vensy.Application.Common.Error;

namespace Vensy.Application.Authentication.Queries;


public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<LoginResult>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public LoginQueryHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<ErrorOr<LoginResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        //  Check if user login is successful
        var user = await _userManager.FindByEmailAsync(request.Username);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return AppError.UnauthorizedError(message: "Invalid username or password", code: "Invalid.Email");
        };

        //  get user roles
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>(){
            new Claim(ClaimTypes.Name, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var role in userRoles)
        {
            authClaims.Add(new(ClaimTypes.Role, role));
        }

        // create refresh and access tokens
        string accessToken = _jwtService.GenerateAccessToken(claims: authClaims);
        string refreshToken = _jwtService.GenerateRefreshToken(claims: authClaims);

        // return login result
        return new LoginResult(user.Email ?? "", user.Lastname, user.Firstname, user.UserName ?? "", accessToken, refreshToken);
    }


}