using System.Data.OleDb;
using MediatR;

namespace Vensy.Application.Authentication.Commands;

public record UpdateUserRefreshTokenCommand(string? OldToken, string NewToken, string Email) : IRequest;