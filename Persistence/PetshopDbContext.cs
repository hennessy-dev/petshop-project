using System.Reflection;
using Microsoft.EntityFrameworkCore;

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
}