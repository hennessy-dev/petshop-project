using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class PetRepository : GenericRepository<Pet>, IPet
    {
        private readonly PetshopDbContext _context;

        public PetRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}