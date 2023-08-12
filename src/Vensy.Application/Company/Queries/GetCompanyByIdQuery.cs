using ErrorOr;
using MediatR;
using Vensy.Application.Company.Results;

namespace Vensy.Application.Company.Queries;

public record GetCompanyByIdQuery(int Id):IRequest<ErrorOr<GetCompanyByIdResult>>;