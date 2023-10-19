using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class MedicaltreatmentConfiguration : IEntityTypeConfiguration<Medicaltreatment>
    {
        public void Configure(EntityTypeBuilder<Medicaltreatment> builder)
        {
            builder.ToTable("medicaltreatment");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasIndex(e => e.AppointmentId, "IX_MedicalTreatment_AppointmentId");

            builder.HasIndex(e => e.MedicineId, "IX_MedicalTreatment_MedicineId");

            builder.Property(e => e.AdministrationDate).HasColumnType("datetime");
            builder.Property(e => e.Comment)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("comment");

            builder.HasOne(d => d.Appointment).WithMany(p => p.Medicaltreatments)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_MedicalTreatment_Appointment_AppointmentId");

            builder.HasOne(d => d.Medicine).WithMany(p => p.Medicaltreatments)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_MedicalTreatment_Medicine_MedicineId");
        }
    }
}