namespace Vensy.Contracts.Company;


public record GetCompanyByIdResponse(
    int Id,
    string Name,
    string Phone,
    string Email,
    string Address
);