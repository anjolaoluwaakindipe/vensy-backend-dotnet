using Vensy.Application.Common.Interfaces.Persistence;
using Vensy.Domain.Models;

namespace Vensy.Infrastructure.Persistence;


public class CompanyRepository : ICompanyRepository
{
    private readonly Context _context;

    public CompanyRepository(Context context)
    {
        _context = context;
    }

    public Company SaveCompany(Company company)
    {
        _context.Add(company);
        _context.SaveChanges();
        return company;
    }

    public async Task<Company> SaveCompanyAsync(Company company)
    {
        await _context.AddAsync(company);
        await _context.SaveChangesAsync();
        return company;
    }
}