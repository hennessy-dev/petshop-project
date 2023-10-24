using System.Data.Common;
using Application.Repository;
using Domain.Interfaces;
using Persistence;
namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly PetshopDbContext _dbContext;
    public UnitOfWork(PetshopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IRolRepository _rols;
    public IRolRepository Rols 
    {
        get
        {
            _rols ??= new RolRepository(_dbContext);
            return _rols;
        }
    }

    private ISpecie _Specie;
    public ISpecie Species
    {
        get { 
            _Specie ??= new SpecieRepository(_dbContext);
                return _Specie;
            }
    }
    private IUserRepository _users;
    public IUserRepository Users    
    {
        get
        {
            _users ??= new UserRepository(_dbContext);
            return _users;
        }
    }

    private IAppointment _Appointment;
    public IAppointment Appointment
    {
        get { 
            _Appointment ??= new AppointmentRepository(_dbContext);
                return _Appointment;
            }
    }

    private IBreed _Breed;
    public IBreed Breed
    {
        get { 
            _Breed ??= new BreedRepository(_dbContext);
                return _Breed;
            }
    }

    private IMedicalTreatment _MedicalTreatment;
    public IMedicalTreatment MedicalTreatment
    {
        get { 
            _MedicalTreatment ??= new MedicalTreatmentRepository(_dbContext);
                return _MedicalTreatment;
            }
    }

    private IMedicine _Medicine;
    public IMedicine Medicine
    {
        get { 
            _Medicine ??= new MedicineRepository(_dbContext);
                return _Medicine;
            }
    }

    private IOwner _Owner;
    public IOwner Owner
    {
        get { 
            _Owner ??= new OwnerRepository(_dbContext);
                return _Owner;
            }
    }

    private IPet _Pet;
    public IPet Pet
    {
        get { 
            _Pet ??= new PetRepository(_dbContext);
                return _Pet;
            }
    }

    private IPurchasedMedicine _PurchasedMedicine;
    public IPurchasedMedicine PurchasedMedicine
    {
        get { 
            _PurchasedMedicine ??= new PurchasedMedicineRepository(_dbContext);
                return _PurchasedMedicine;
            }
    }

    private ISoldMedicine _SoldMedicine;
    public ISoldMedicine SoldMedicine
    {
        get { 
            _SoldMedicine ??= new SoldMedicineRepository(_dbContext);
                return _SoldMedicine;
            }
    }

    private ISupplier _Supplier;
    public ISupplier Supplier
    {
        get { 
            _Supplier ??= new SupplierRepository(_dbContext);
                return _Supplier;
            }
    }

    private IVeterinarian _Veterinarian;
    public IVeterinarian Veterinarian
    {
        get { 
            _Veterinarian ??= new VeterinarianRepository(_dbContext);
                return _Veterinarian;
            }
    }

    public int Save()
    {
        return _dbContext.SaveChanges();
    }
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }
}