namespace Domain.Interfaces;
public interface IUnitOfWork
{
    IRolRepository Rols { get; }
    ISpecie Species { get; }
    IUserRepository Users { get; }
    IAppointment Appointment { get; }
    IBreed Breed { get; }
    IMedicalTreatment MedicalTreatment{ get; }
    IMedicine Medicine { get; }
    IOwner Owner { get; }
    IPet Pet { get; }
    IPurchasedMedicine PurchasedMedicine { get; }
    ISoldMedicine SoldMedicine { get; }
    ISupplier Supplier { get; }
    IVeterinarian Veterinarian { get; }
    Task<int> SaveAsync();
}