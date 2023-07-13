using System.Net;
using Vensy.Application.Interfaces;

namespace Vensy.Application.Common.Error;

public class UnauthorizedError : ServiceException<string>
{
    public UnauthorizedError(string message) : base(message, HttpStatusCode.Unauthorized)
    {
    }
}