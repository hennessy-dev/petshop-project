using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("appointment");

            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.PetId, "IX_Appointment_PetId");

            builder.HasIndex(e => e.VeterinarianId, "IX_Appointment_VeterinarianId");

            builder.Property(e => e.Date).HasColumnType("datetime");
            builder.Property(e => e.Reason)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(d => d.Pet).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK_Appointment_Pet_PetId");

            builder.HasOne(d => d.Veterinarian).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.VeterinarianId)
                .HasConstraintName("FK_Appointment_Veterinarian_VeterinarianId");
        }
    }
}