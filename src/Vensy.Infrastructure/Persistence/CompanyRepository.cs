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

    public bool DoesCompanyExist(int Id)
    {
        return _context.Companies.Any(company=> company.Id == Id);
    }

    public Company FindCompanyById(int Id)
    {
        return _context.Companies.First(company => company.Id ==Id);
    }

    public async Task<Company> FindCompanyByIdAsync(int Id)
    {
        return _context.Companies.First(company => company.Id ==Id);
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

    public Company UpdateCompany(Company company)
    {
        _context.Update(company);
        _context.SaveChanges();
        return company;
    }

    public async Task<Company> UpdateCompanyAsync(Company company)
    {
        _context.Update(company);
        await _context.SaveChangesAsync();
        return company;
    }

}