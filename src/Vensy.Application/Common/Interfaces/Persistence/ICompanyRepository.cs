using Vensy.Domain.Models;
namespace Vensy.Application.Common.Interfaces.Persistence;


public interface ICompanyRepository
{
    public Domain.Models.Company SaveCompany(Domain.Models.Company company);
    public Task<Domain.Models.Company> SaveCompanyAsync(Domain.Models.Company company);
}