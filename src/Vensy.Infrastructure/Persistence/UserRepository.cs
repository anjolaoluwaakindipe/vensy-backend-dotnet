using System;
using Vensy.Application.Interfaces.Persistence;
using Vensy.Domain.Models;

namespace Vensy.Infrastructure.Persistence;


public class UserRepository : IUserRepository
{
    private readonly Context _context;


    public UserRepository(Context context)
    {
        _context = context;
    }


    public User? FindByEmail(string email)
    {
        return _context.Users.Where<User>(user => user.Email == email).FirstOrDefault();
    }

    public void Save(User user)
    {
        _context.Users.Add(user);
    }
}