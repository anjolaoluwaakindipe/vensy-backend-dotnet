using System.Data;
using Vensy.Application.Interfaces;
using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace Vensy.Application.Common.Error;

public static partial class AppError{
    public static ErrorOr.Error ConflictError(string message, string code){
        return ErrorOr.Error.Custom(
            StatusCodes.Status409Conflict,
            code,
            message
        );
    }
    public static ErrorOr.Error ConflictError(string message){
        return ErrorOr.Error.Custom(
            StatusCodes.Status409Conflict,
            "Conflict.Error",
            message
        );
    }
}