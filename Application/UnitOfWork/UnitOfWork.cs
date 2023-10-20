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

    private IMedicaltreatment _Medicaltreatment;
    public IMedicaltreatment Medicaltreatment
    {
        get { 
            _Medicaltreatment ??= new MedicaltreatmentRepository(_dbContext);
                return _Medicaltreatment;
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

    private IPurchasedmedicine _Purchasedmedicine;
    public IPurchasedmedicine Purchasedmedicine
    {
        get { 
            _Purchasedmedicine ??= new PurchasedmedicineRepository(_dbContext);
                return _Purchasedmedicine;
            }
    }

    private ISoldmedicine _Soldmedicine;
    public ISoldmedicine Soldmedicine
    {
        get { 
            _Soldmedicine ??= new SoldmedicineRepository(_dbContext);
                return _Soldmedicine;
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