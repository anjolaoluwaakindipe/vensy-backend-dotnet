using ErrorOr;
using MediatR;
using Vensy.Application.Authentication.Common;

namespace Vensy.Application.Authentication.Queries;



public record ValidateRefreshTokenQuery(string RefreshToken) : IRequest<ErrorOr<ValidateRefreshTokenResult>>;