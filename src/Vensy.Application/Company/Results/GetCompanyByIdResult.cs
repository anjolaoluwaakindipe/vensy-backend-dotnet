namespace Vensy.Application.Company.Results;

public record GetCompanyByIdResult(int Id, string Name, string Email, string Phone, string Address);