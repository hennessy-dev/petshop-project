using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence;
public class PetshopDbContext : DbContext
{
    public PetshopDbContext(DbContextOptions options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<Breed> Breeds { get; set; }
    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }
    public virtual DbSet<Medicaltreatment> Medicaltreatments { get; set; }
    public virtual DbSet<Medicine> Medicines { get; set; }
    public virtual DbSet<Owner> Owners { get; set; }
    public virtual DbSet<Pet> Pets { get; set; }
    public virtual DbSet<Purchasedmedicine> Purchasedmedicines { get; set; }
    public virtual DbSet<Refreshtoken> Refreshtokens { get; set; }
    public virtual DbSet<Rol> Rols { get; set; }
    public virtual DbSet<Soldmedicine> Soldmedicines { get; set; }
    public virtual DbSet<Species> Species { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Veterinarian> Veterinarians { get; set; }


}