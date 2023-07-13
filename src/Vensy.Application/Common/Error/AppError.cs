using System.Net;

namespace Vensy.Application.Interfaces;


public class ServiceException<T> : Exception
{
    public ServiceException(string message, HttpStatusCode statusCode) : base(message)
    {
        this.Status = statusCode;
    }

    public ServiceException(string message, Exception innerException, HttpStatusCode status) : base(message, innerException) 
    {
        this.Status = status;
     }

    public ServiceException(string message, T errorMessage, HttpStatusCode statusCode) : base(message)
    {
        this.ErrorDescription = errorMessage;
        this.Status = statusCode;
    }

    public HttpStatusCode Status { get; }

    public T ErrorDescription { get; }
}