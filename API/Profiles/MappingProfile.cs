using API.DTOs;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Appointment, AppointmentDto>().ReverseMap();
        CreateMap<Breed, BreedDto>().ReverseMap();
        CreateMap<MedicalTreatment, MedicalTreatmentDto>();
        CreateMap<Medicine ,MedicineDto>().ReverseMap();
        CreateMap<Owner ,OwnerDto>().ReverseMap();
        CreateMap<Owner, OwnerWithPetDto>().ReverseMap();
        CreateMap<Pet, PetDto>().ReverseMap();
        CreateMap<Pet, PetWithOwnerDto>().ReverseMap();
        CreateMap<PurchasedMedicine, PurchasedMedicineDto>().ReverseMap();
        CreateMap<SoldMedicine, SoldMedicineDto>().ReverseMap();
        CreateMap<Specie, SpeciesDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
        CreateMap<Veterinarian, VeterinarianDto>().ReverseMap();
    }
}