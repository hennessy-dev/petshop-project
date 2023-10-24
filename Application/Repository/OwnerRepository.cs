using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<(IEnumerable<Owner> owners, int total)> OwnerWithPet(int pageIndex, int pageSize)
        {
            var registers = await _context.Owners.Include(o=>o.Pets).ToListAsync();
            var Fregisters = registers.Skip((pageIndex -1) * pageSize).Take(pageSize).ToList();
            int totalRegisters = registers.Count;
            return (Fregisters, totalRegisters);
            throw new NotImplementedException();
        }
    }
}