using Domain.Entities;
namespace Domain.Interfaces;
public interface IPet : IGenericRepository<Pet>
{
    Task<(IEnumerable<Pet> registers, int totalRegisters)> GetPetsBySpecies(int pageIndex, int pageSize,string species);
    Task<(IEnumerable<Pet> registers, int totalRegisters)> GetPetsByBreed(int pageIndex, int pageSize,string Breed);
    Task<(IEnumerable<Pet> registers, int totalRegisters)> GetPetsByAppointmentReason(int pageIndex, int pageSize,string Reason);
    Task<(IEnumerable<Pet> registers, int totalRegisters)> GetPetsByVeterinarian(int pageIndex, int pageSize,string VeterinarianName);
}