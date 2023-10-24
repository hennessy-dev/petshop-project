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

    public async Task<User> GetByRefreshTokenAsync(string RefreshToken)
    {
        return await _context.Users
            .Include(u => u.Rols)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == RefreshToken));
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Username)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }
}