using System.Net;
using Microsoft.AspNetCore.Http;
using Vensy.Application.Interfaces;

namespace Vensy.Application.Common.Error;


public static partial class AppError{
    public static ErrorOr.Error NotFoundError(string message, string code){
        return ErrorOr.Error.Custom(
            StatusCodes.Status404NotFound,
            code,
            message
        );
    }
    public static ErrorOr.Error NotFoundError(string message){
        return ErrorOr.Error.Custom(
            StatusCodes.Status404NotFound,
            "Unauthorized.Error",
            message
        );
    }
}