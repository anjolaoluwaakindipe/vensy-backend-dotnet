using ErrorOr;
using MediatR;
using Vensy.Application.Authentication.Common;

namespace Vensy.Application.Authentication.Commands;

public record RegisterCommand(string Email, string Password, string Firstname, string Lastname, string Username): IRequest<ErrorOr<RegisterResult>>;