using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class VeterinarianRepository : GenericRepository<Veterinarian>, IVeterinarian
    {
        private readonly PetshopDbContext _context;

        public VeterinarianRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}