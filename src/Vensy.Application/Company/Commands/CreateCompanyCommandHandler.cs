using System.Net.Cache;
using System.ComponentModel;
using System.Reflection.Metadata;
using ErrorOr;
using MediatR;
using Vensy.Application.Common.Error;
using Vensy.Application.Common.Interfaces.Persistence;
using Vensy.Application.Company.Results;

namespace Vensy.Application.Company.Commands;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, ErrorOr<CreateCompanyResult>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserRepository _userRepository;
    public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IUserRepository userRepository)
    {
        _companyRepository = companyRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<CreateCompanyResult>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var existingUser = _userRepository.FindByEmail(request.UserEmail);

        if (existingUser is null) return AppError.BadRequestError("User does not exist", "Invalid.User");

        var company = new Domain.Models.Company() { Name = request.Name, Address = request.Address, Email = request.Email, Phone = request.Phone, ApplicationUserId = existingUser.Id };

        var savedCompany = _companyRepository.SaveCompany(company);

        return new CreateCompanyResult(savedCompany.Id, savedCompany.Name, savedCompany.Email, savedCompany.Phone, savedCompany.Address);
    }
}