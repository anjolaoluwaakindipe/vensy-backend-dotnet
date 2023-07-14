using System.Reflection.Metadata;
using System;
using Vensy.Application.Interfaces.Persistence;
using Vensy.Domain.Models;
using System.Security.Principal;

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

    public void Save(ApplicationUser user)
    {
        _context.Users.Add(user);
    }
}