using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class MedicaltreatmentRepository : GenericRepository<Medicaltreatment>, IMedicaltreatment
    {
        private readonly PetshopDbContext _context;

        public MedicaltreatmentRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}