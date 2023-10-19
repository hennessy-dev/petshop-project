using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("owner");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(70);
            builder.Property(e => e.Telephone)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}