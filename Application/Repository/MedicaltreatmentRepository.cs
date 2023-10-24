using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class MedicalTreatmentRepository : GenericRepository<MedicalTreatment>, IMedicalTreatment
    {
        private readonly PetshopDbContext _context;

        public MedicalTreatmentRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}