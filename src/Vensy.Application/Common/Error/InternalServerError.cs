using System.Net;
using Vensy.Application.Interfaces;

namespace Vensy.Application.Common.Error;


public class InternalServerError : ServiceException<string>
{
    public InternalServerError(string message, Exception innerException) : base(message, innerException, HttpStatusCode.InternalServerError)
    {
    }

    public InternalServerError(string message) : base(message, HttpStatusCode.InternalServerError)
    {
    }
}