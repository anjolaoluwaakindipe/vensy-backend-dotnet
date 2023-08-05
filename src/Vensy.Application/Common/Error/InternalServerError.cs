
using Microsoft.AspNetCore.Http;

namespace Vensy.Application.Common.Error;

public static partial class AppError{
    public static ErrorOr.Error InternalServerError(string message, string code){
        return ErrorOr.Error.Custom(
            StatusCodes.Status500InternalServerError,
            code,
            message
        );
    }
    public static ErrorOr.Error InternalServerError(string message){
        return ErrorOr.Error.Custom(
            StatusCodes.Status500InternalServerError,
            "InternalServer.Error",
            message
        );
    }
}