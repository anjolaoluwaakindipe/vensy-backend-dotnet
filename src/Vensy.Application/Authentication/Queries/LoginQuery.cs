using ErrorOr;
using MediatR;
using Vensy.Application.Authentication.Common;

namespace Vensy.Application.Authentication.Queries;



public record LoginQuery(string Username, string Password) : IRequest<ErrorOr<LoginResult>>;