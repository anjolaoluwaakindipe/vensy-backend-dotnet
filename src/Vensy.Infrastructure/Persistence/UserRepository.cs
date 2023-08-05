using Vensy.Domain.Models;
using Vensy.Application.Common.Interfaces.Persistence;

namespace Vensy.Infrastructure.Persistence;


public class UserRepository : IUserRepository
{
    private readonly Context _context;


    public UserRepository(Context context)
    {
        _context = context;
    }


    public ApplicationUser? FindByEmail(string email)
    {
        return _context.Users.Where<ApplicationUser>(user => user.Email == email).FirstOrDefault();
    }

    public ApplicationUser? FindUserByRefreshToken(string RefreshToken, string Email)
    {
        return _context.Users.Where(predicate: user => user.Email == Email && user.RefreshTokens.Any((refreshToken => refreshToken.Token == RefreshToken)))
                             .FirstOrDefault<ApplicationUser>();

    }

    public async Task Save(ApplicationUser user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    void IUserRepository.Save(ApplicationUser user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}