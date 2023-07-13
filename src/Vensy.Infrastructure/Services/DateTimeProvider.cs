using Vensy.Application.Common.Interfaces.Services;

namespace Vensy.Infrastructure.Services;


public class DateTimeProvider: IDateTimeProvider
{
    public DateTime UtcNow() => DateTime.UtcNow;
}