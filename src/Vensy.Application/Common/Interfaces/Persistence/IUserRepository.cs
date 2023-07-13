using Vensy.Domain.Models;
namespace Vensy.Application.Interfaces.Persistence;


public interface IUserRepository
{
    public User? FindByEmail(string email);
    public void Save(User user);
}