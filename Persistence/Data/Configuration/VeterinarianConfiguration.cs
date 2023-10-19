using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class VeterinarianConfiguration : IEntityTypeConfiguration<Veterinarian>
    {
        public void Configure(EntityTypeBuilder<Veterinarian> builder)
        {
            builder.ToTable("veterinarian");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(e => e.Speciality)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(e => e.Telephone)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}