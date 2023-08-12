using Vensy.Domain.Models;
namespace Vensy.Application.Common.Interfaces.Persistence;


public interface ICompanyRepository
{
    public Domain.Models.Company SaveCompany(Domain.Models.Company company);
    public Task<Domain.Models.Company> SaveCompanyAsync(Domain.Models.Company company);
    public Task<Domain.Models.Company> UpdateCompanyAsync(Domain.Models.Company company);
    public Domain.Models.Company UpdateCompany(Domain.Models.Company company);
    public bool DoesCompanyExist(int Id);
    public Domain.Models.Company FindCompanyById(int Id);
    public Task<Domain.Models.Company> FindCompanyByIdAsync(int Id);
}