using Microsoft.AspNetCore.Http;

namespace Vensy.Application.Common.Error;

public static partial class AppError
{
    public static ErrorOr.Error BadRequestError(string message, string code)
    {
        return ErrorOr.Error.Custom(
            StatusCodes.Status400BadRequest,
            code,
            message
        );
    }
    public static ErrorOr.Error BadRequestError(string message)
    {
        return ErrorOr.Error.Custom(
            StatusCodes.Status400BadRequest,
            "Validation.Error",
            message
        );
    }
}