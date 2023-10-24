using Domain.Entities;
namespace Domain.Interfaces;
public interface IVeterinarian : IGenericRepository<Veterinarian>
{
    Task<(IEnumerable<Veterinarian> registers, int totalRegisters)> GetVeterinariansBySpeciality(int pageIndex, int pageSize,string speciality);
}