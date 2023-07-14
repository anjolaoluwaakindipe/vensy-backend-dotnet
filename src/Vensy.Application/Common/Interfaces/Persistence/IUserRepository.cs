using Vensy.Domain.Models;
namespace Vensy.Application.Interfaces.Persistence;


public interface IUserRepository
{
    public ApplicationUser? FindByEmail(string email);
    public void Save(ApplicationUser user);
    public ApplicationUser? FindUserByRefreshToken(string RefreshToken, string Email);
}