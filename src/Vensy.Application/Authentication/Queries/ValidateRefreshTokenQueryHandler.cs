using System.Net;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Vensy.Application.Authentication.Common;
using Vensy.Application.Interfaces.Persistence;

namespace Vensy.Application.Authentication.Queries;


public class ValidateRefreshTokenQueryHandler : IRequestHandler<ValidateRefreshTokenQuery, ErrorOr<ValidateRefreshTokenResult>>
{
    private readonly IUserRepository _userRepository;
    public ValidateRefreshTokenQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<ValidateRefreshTokenResult>> Handle(ValidateRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        if(request.RefreshToken.Trim() == "" )
        {
            return Error.Custom(StatusCodes.Status401Unauthorized, "RefreshToken.Invalid", "No refresh token provided");
        }

        // Validate Refresh token

        // Get princpal/email from JWT

        // Get user from JWT

        // check if refresh token is present in database and belongs to user

        // if not (token reuse) clear all refresh tokens

        // generate access and refresh token

        return new ValidateRefreshTokenResult("hello", "ha","hajsdf", "asdhf", "asdf", "asdf");
    }
}