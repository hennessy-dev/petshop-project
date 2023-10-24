using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class PetRepository : GenericRepository<Pet>, IPet
    {
        private readonly PetshopDbContext _context;

        public PetRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Pet> registers, int totalRegisters)> GetPetsBySpecies(int pageIndex, int pageSize, string species)
        {
            var registers = await _context.Pets.Include(p=>p.Breed).ThenInclude(b=>b.Species).Where(p=>p.Breed.Species.Name
            .Equals(species)).Skip((pageIndex -1) * pageSize).Take(pageSize).ToListAsync();
            int totalRegisters = registers.Count();
            return (registers, totalRegisters);
        }
        public async Task<(IEnumerable<Pet> registers, int totalRegisters)> GetPetsByAppointmentReason(int pageIndex, int pageSize, string reason)
        {
            var appointments = await _context.Appointments.Where(a=>a.Reason.ToLower().Equals(reason.ToLower())).Select(a=>a.PetId).Distinct().ToListAsync();
            var registers = await _context.Pets.Where(p=> appointments.Contains(p.Id)).Skip((pageIndex -1) * pageSize).Take(pageSize).ToListAsync();
            int totalRegisters = appointments.Count;
            return (registers, totalRegisters);
        }

        public async Task<(IEnumerable<Pet> registers, int totalRegisters)> GetPetsByVeterinarian(int pageIndex, int pageSize, string VeterinarianName)
        {
            var appointments = await _context.Appointments.Where(a=>a.Veterinarian.Name.ToLower().Equals(VeterinarianName.ToLower())).Select(a=>a.PetId).Distinct().ToListAsync();
            var registers = await _context.Pets.Where(p=> appointments.Contains(p.Id)).Skip((pageIndex -1) * pageSize).Take(pageSize).ToListAsync();
            int totalRegisters = appointments.Count;
            return (registers, totalRegisters);
        }
        public async Task<(IEnumerable<Pet> registers, int totalRegisters)> GetPetsByBreed(int pageIndex, int pageSize, string Breed)
        {
            var registers = await _context.Pets
                .Include(m => m.Owner)
                .ToListAsync();
            var Fregisters = registers.Where(p=>p.Breed.Name.Equals(Breed)).Skip((pageIndex -1) * pageSize).Take(pageSize);
            int totalRegisters = registers.Count;
            return (Fregisters, totalRegisters);
        }
    }
}