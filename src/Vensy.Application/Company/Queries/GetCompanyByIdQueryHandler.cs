using System.Net.Cache;
using ErrorOr;
using MediatR;
using Vensy.Application.Common.Error;
using Vensy.Application.Common.Interfaces.Persistence;
using Vensy.Application.Company.Results;

namespace Vensy.Application.Company.Queries;


public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, ErrorOr<GetCompanyByIdResult>>
{
    private readonly ICompanyRepository _companyRepository;
    public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    public async Task<ErrorOr<GetCompanyByIdResult>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        Domain.Models.Company existingCompany = _companyRepository.FindCompanyById(request.Id);
        if (existingCompany is null)
        {
            return AppError.NotFoundError($"Company with Id {request.Id} not found");
        }
        return new GetCompanyByIdResult(existingCompany.Id, existingCompany.Name, existingCompany.Email, existingCompany.Phone, existingCompany.Address);
    }
}