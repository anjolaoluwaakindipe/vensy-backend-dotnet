using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vensy.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected ActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.Count > 0 && errors.All(err => err.Type == ErrorType.Validation))
        {
            return ValidationProblems(errors);
        }

        var firstError = errors[0];

        return Problem(firstError);
    }

    private ActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private ActionResult ValidationProblems(List<Error> errors)
    {
        ModelStateDictionary modelStateDictionary = new();

        foreach (var err in errors)
        {
            modelStateDictionary.AddModelError(err.Code, err.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}