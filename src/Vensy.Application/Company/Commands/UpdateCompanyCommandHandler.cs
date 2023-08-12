using System.Net;
using System.Net.Cache;
using ErrorOr;
using MediatR;
using Vensy.Application.Common.Error;
using Vensy.Application.Common.Interfaces.Persistence;
using Vensy.Application.Company.Results;
using Vensy.Domain.Models;

namespace Vensy.Application.Company.Commands;



public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, ErrorOr<UpdateCompanyResult>>
{
    private readonly ICompanyRepository _companyRepository;
    public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<ErrorOr<UpdateCompanyResult>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        Domain.Models.Company existingCompany = _companyRepository.FindCompanyById(request.Id);
        if (existingCompany is null)
        {
            return AppError.NotFoundError($"Company with Id {request.Id} does not exist");
        }

        if (request.Name is not null)
        {
            existingCompany.Name = request.Name;
        }

        if (request.Phone is not null)
        {
            existingCompany.Phone = request.Phone;
        }

        if (request.Address is not null)
        {
            existingCompany.Address = request.Address;
        }

        Domain.Models.Company updatedCompany = await _companyRepository.UpdateCompanyAsync(existingCompany);

        return new UpdateCompanyResult(Address: updatedCompany.Address, Id: updatedCompany.Id, Name: updatedCompany.Name, Phone: updatedCompany.Name);
    }
}