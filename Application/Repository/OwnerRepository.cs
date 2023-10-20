using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class OwnerRepository : GenericRepository<Owner>, IOwner
    {
        private readonly PetshopDbContext _context;

        public OwnerRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}