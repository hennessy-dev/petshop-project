using Domain.Entities;
namespace Domain.Interfaces;
public interface IOwner : IGenericRepository<Owner>
{
    Task<(IEnumerable<Owner> owners,int total)> OwnerWithPet (int pageIndex, int pageSize);
}