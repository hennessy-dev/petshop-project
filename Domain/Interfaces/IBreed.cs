using Domain.Entities;
namespace Domain.Interfaces;
public interface IBreed : IGenericRepository<Breed>
{
    Task<(IEnumerable<Breed>BreedWithManyPets,int Total)> GetHowManyPetsAreInTheBreed(int pageIndex, int pageSize);
}