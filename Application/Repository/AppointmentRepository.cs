using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointment
    {
        private readonly PetshopDbContext _context;

        public AppointmentRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }
    }
}