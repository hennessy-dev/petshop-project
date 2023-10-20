using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly PetshopDbContext _context;

    public RolRepository(PetshopDbContext context) : base(context)
    {
        _context = context;
    }
}