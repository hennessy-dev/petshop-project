using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicine
    {
        private readonly PetshopDbContext _context;

        public MedicineRepository(PetshopDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Medicine> registers, int totalRegisters)> GetMedicinesByLaboratory(int pageIndex, int pageSize,string LaboratoryName)
        {
            var registers = await _context.Medicines.Where(m=>m.Laboratory.Equals(LaboratoryName)).ToListAsync();
            var Fregisters = registers.Skip((pageIndex -1) * pageSize).Take(pageSize);
            int totalRegisters = registers.Count();
            return (Fregisters, totalRegisters);
        }
        public async Task<(IEnumerable<Medicine> registers, int totalRegisters)> GetMedicinesExpensiveThan(int pageIndex, int pageSize)
        {
            var registers = await _context.Medicines.Where(m=>m.Price > 50000).ToListAsync();
            var Fregisters = registers.Skip((pageIndex -1) * pageSize).Take(pageSize);
            int totalRegisters = registers.Count;
            return (Fregisters, totalRegisters);
        }
        public async Task RestMedicine(int MedicineId, int TotalSold)
        {
            try
            {
                Medicine Medicine = await _context.Medicines.FirstOrDefaultAsync(
                    m => m.Id == MedicineId
                );
                Medicine.Stock -= TotalSold;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("Medicine not found");
            }
        }
        public async Task AddMedicine(int MedicineId, int TotalBought)
        {
            try
            {
                Medicine Medicine = await _context.Medicines.FirstOrDefaultAsync(
                    m => m.Id == MedicineId
                );
                Medicine.Stock += TotalBought;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("Medicine not found");
            }
        }
    }
}