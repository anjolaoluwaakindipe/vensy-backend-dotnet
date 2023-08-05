using System.Net;
using Microsoft.AspNetCore.Http;
using Vensy.Application.Interfaces;

namespace Vensy.Application.Common.Error;


public static partial class AppError{
    public static ErrorOr.Error UnauthorizedError(string message, string code){
        return ErrorOr.Error.Custom(
            StatusCodes.Status401Unauthorized,
            code,
            message
        );
    }
    public static ErrorOr.Error UnauthorizedError(string message){
        return ErrorOr.Error.Custom(
            StatusCodes.Status401Unauthorized,
            "Unauthorized.Error",
            message
        );
    }
}