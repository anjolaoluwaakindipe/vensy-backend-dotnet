using System.Net;
using Vensy.Application.Interfaces;

namespace Vensy.Application.Common.Error;


public class ValidationError<T> : ServiceException<T>
{
    public ValidationError(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}