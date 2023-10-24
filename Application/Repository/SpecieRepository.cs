using Application.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using Persistencia;

namespace Application.Repository
{
    public class SpecieRepository : GenericRepository<Specie>, ISpecie
    {
        private readonly PetshopDbContext _context;

        public SpecieRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}