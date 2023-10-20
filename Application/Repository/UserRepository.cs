using System.Runtime.CompilerServices;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly PetshopDbContext _context;

    public UserRepository(PetshopDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users
            .Include(u => u.Rols)
            .Include(u => u.Refreshtokens)
            .FirstOrDefaultAsync(u => u.Username.ToLower() == u.Username.ToLower());
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Username)
            .Include(u => u.Refreshtokens)
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }
}