using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplier
    {
        private readonly PetshopDbContext _context;

        public SupplierRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}