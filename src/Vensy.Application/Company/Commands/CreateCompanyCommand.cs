using ErrorOr;
using MediatR;
using Vensy.Application.Company.Results;

namespace Vensy.Application.Company.Commands;

public record CreateCompanyCommand(string UserEmail, string Name, string Email, string Phone, string Address):IRequest<ErrorOr<CreateCompanyResult>>;