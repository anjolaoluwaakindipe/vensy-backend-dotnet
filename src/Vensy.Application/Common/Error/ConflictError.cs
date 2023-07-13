using System.Net;
using Vensy.Application.Interfaces;

namespace Vensy.Application.Common.Error;


public class ConflictError : ServiceException<string>
{
    public ConflictError(string message) : base(message, HttpStatusCode.Conflict)
    {
    }
}