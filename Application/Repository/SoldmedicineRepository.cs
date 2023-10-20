using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class SoldmedicineRepository : GenericRepository<Soldmedicine>, ISoldmedicine
    {
        private readonly PetshopDbContext _context;

        public SoldmedicineRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}