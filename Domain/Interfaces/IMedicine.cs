using Domain.Entities;
namespace Domain.Interfaces;
public interface IMedicine : IGenericRepository<Medicine>
{
    Task<(IEnumerable<Medicine> registers, int totalRegisters)> GetMedicinesByLaboratory(int pageIndex, int pageSize,string LaboratoryName);
    Task<(IEnumerable<Medicine> registers, int totalRegisters)> GetMedicinesExpensiveThan(int pageIndex, int pageSize);
    Task RestMedicine (int MedicineId, int TotalSold);
    Task AddMedicine (int MedicineId, int TotalBought);
}