using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class PurchasedMedicineRepository : GenericRepository<PurchasedMedicine>, IPurchasedMedicine
    {
        private readonly PetshopDbContext _context;

        public PurchasedMedicineRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}