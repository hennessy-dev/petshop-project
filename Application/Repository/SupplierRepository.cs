using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<(IEnumerable<Supplier> suppliers, int totals)> GetWhoSells(int pageIndex, int pageSize, string medicineName)
        {
            var registers = await _context.Suppliers.Where (s=>s.Medicines.Any(m=>m.Name == medicineName)).ToListAsync();
            var Fregisters = registers.Skip((pageIndex -1) * pageSize).Take(pageSize);
            var a = registers.Count;
            return (Fregisters,a);
        }
    }
}