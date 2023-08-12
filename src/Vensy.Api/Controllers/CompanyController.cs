using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vensy.Application.Company.Commands;
using Vensy.Application.Company.Queries;
using Vensy.Application.Company.Results;
using Vensy.Contracts.Company;

namespace Vensy.Api.Controllers;

[Route("api/v1/company")]
public class CompanyController : ApiControllerBase
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
        var command = new CreateCompanyCommand("", request.Name, request.Email, request.Phone, request.Address);
        var result = await _sender.Send(command);

        return result.Match(
            value => Created($"api/v1/company/{value.Id}",
                             new CreateCompanyResponse(Id: value.Id, Address: value.Address, Name: value.Name, Phone: value.Phone)),
            err => Problem(err)
        );
    }

    [HttpPut("/{companyId}")]
    [Authorize]
    public async Task<IActionResult> Update(int companyId, UpdateCompanyRequest request)
    {
        var command = new UpdateCompanyCommand(companyId, request.Name, request.Phone, request.Address);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(new UpdateCompanyResponse(value.Id, value.Name, value.Address, value.Phone)),
            err => Problem(err)
        );
    }

    [HttpGet("/{companyId}")]
    public async Task<IActionResult> GetById(int companyId)
    {
        var command = new GetCompanyByIdQuery(companyId);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(
                new GetCompanyByIdResponse(
                    value.Id,
                    value.Name,
                    value.Phone,
                    value.Email,
                    value.Address
                )
            ),
            err => Problem(err)
        );
    }
}