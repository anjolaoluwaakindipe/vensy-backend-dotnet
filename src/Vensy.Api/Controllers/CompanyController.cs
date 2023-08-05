using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vensy.Contracts.Company;

namespace Vensy.Api.Controllers;

[Route("api/v1/company")]
public class CompanyController:ApiControllerBase
{

    private readonly IHttpContextAccessor _httpContAcc;
    private readonly ISender _sender;

    public CompanyController(ISender mediator, IHttpContextAccessor httpContextAccessor)
    {
        _sender = mediator;
        _httpContAcc = httpContextAccessor;
    }

    [HttpPost("/")]
    [Authorize]
    public async Task<IActionResult> Create(CreateCompanyRequest request)
    {
        
        return Ok();
    }
}