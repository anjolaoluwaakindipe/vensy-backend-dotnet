using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Vensy.Application.Authentication.Common;
using Vensy.Application.Common.Interfaces.Services;
using Vensy.Application.Interfaces.Persistence;
using Vensy.Domain.Models;

namespace Vensy.Application.Authentication.Queries;



public class ValidateRefreshTokenQueryHandler : IRequestHandler<ValidateRefreshTokenQuery, ErrorOr<ValidateRefreshTokenResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly UserManager<ApplicationUser> _userManager;
    public ValidateRefreshTokenQueryHandler(IUserRepository userRepository, IJwtService jwtService, UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _userManager = userManager;
    }
    public async Task<ErrorOr<ValidateRefreshTokenResult>> Handle(ValidateRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        if(request.RefreshToken.Trim() == "" )
        {
            return Error.Custom(StatusCodes.Status400BadRequest, "RefreshToken.Empty", "No refresh token provided");
        }

        // Validate Refresh token
        var principal =  _jwtService.GetClaimPrincipalFromRefreshToken(request.RefreshToken);


        // Get princpal/email from JWT
        if (principal is null) return Error.Custom(StatusCodes.Status401Unauthorized, "RefreshToken.Invalid", "Invalid Refresh Token");

        // Get user from JWT
        var email = principal.Identity?.Name;
        if (email is null) return Error.Custom(StatusCodes.Status401Unauthorized, "RefreshToken.Invalid.NullPrincipalName", "Invalid Refresh Token");

        // check if refresh token is present in database and belongs to user
        var user = _userRepository.FindByEmail(email);
        if (user is null) return Error.Custom(StatusCodes.Status401Unauthorized, "RefreshToken.Invalid.NullUser", "Invalid Refresh Token");

        // if not (token reuse) clear all refresh tokens
        var oldToken = user.RefreshTokens.Where(refresh => refresh.Token == request.RefreshToken).First();
        if (oldToken is null)
        {
            user.RefreshTokens.Clear();
            _userRepository.Save(user);
            return Error.Custom(StatusCodes.Status401Unauthorized, "RefreshToken.Invalid.TokenReuse", "Invalid Refresh Token");
        }
        
        // generate access and refresh token
        var (accessToken, refreshToken) = await GenerateTokens(user);

        return new ValidateRefreshTokenResult(user.UserName!, user.Firstname,user.Lastname, user.Email!, accessToken, refreshToken);
    }

    private async Task<Tokens> GenerateTokens(ApplicationUser user)
    {
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

        return new Tokens(accessToken, refreshToken);
    }

}