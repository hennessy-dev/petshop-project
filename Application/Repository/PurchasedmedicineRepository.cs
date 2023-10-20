using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class PurchasedmedicineRepository : GenericRepository<Purchasedmedicine>, IPurchasedmedicine
    {
        private readonly PetshopDbContext _context;

        public PurchasedmedicineRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}