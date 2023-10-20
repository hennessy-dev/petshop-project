using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class BreedRepository : GenericRepository<Breed>, IBreed
    {
        private readonly PetshopDbContext _context;

        public BreedRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Breed> BreedWithManyPets, int Total)> GetHowManyPetsAreInTheBreed(int pageIndex, int pageSize)
        {
            var breeds =await _context.Breeds.Include(b=>b.Pets).ToListAsync();
            int total = breeds.Count;
            var Fbreeds = breeds.Skip((pageIndex -1) * pageSize).Take(pageSize);
            return (Fbreeds,total);
        }
    }
}