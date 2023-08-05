namespace Vensy.Contracts.Company;


public record CreateCompanyRequest(string Name, string Email, string Phone, string Address);