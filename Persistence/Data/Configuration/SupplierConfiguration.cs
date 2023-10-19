using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("supplier");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(e => e.Telephone)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}