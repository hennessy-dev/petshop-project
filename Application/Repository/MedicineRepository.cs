using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicine
    {
        private readonly PetshopDbContext _context;

        public MedicineRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}