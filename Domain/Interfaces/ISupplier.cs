using Domain.Entities;
namespace Domain.Interfaces;
public interface ISupplier : IGenericRepository<Supplier>
{
    public Task<(IEnumerable<Supplier> suppliers, int totals)> GetWhoSells(int pageIndex, int pageSize, string medicineName);
}