using ErrorOr;
using MediatR;
using Vensy.Application.Company.Results;

namespace Vensy.Application.Company.Commands;

public record UpdateCompanyCommand(int Id, string? Name, string? Phone, string? Address):IRequest<ErrorOr<UpdateCompanyResult>>;