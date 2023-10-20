namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRolRepository Rols { get; }
        IUserRepository Users { get; }
        IAppointment Appointment { get; }
        IBreed Breed { get; }
        IMedicaltreatment Medicaltreatment{ get; }
        IMedicine Medicine { get; }
        IOwner Owner { get; }
        IPet Pet { get; }
        IPurchasedmedicine Purchasedmedicine { get; }
        ISoldmedicine Soldmedicine { get; }
        ISupplier Supplier { get; }
        IVeterinarian Veterinarian { get; }
        Task<int> SaveAsync();
    }
}