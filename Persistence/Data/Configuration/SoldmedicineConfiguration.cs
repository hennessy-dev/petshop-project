using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class SoldmedicineConfiguration : IEntityTypeConfiguration<Soldmedicine>
    {
        public void Configure(EntityTypeBuilder<Soldmedicine> builder)
        {
            builder.ToTable("Soldmedicine");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasIndex(e => e.MedicineId, "IX_SoldMedicine_MedicineId");

            builder.Property(e => e.SoldDate).HasColumnType("datetime");

            builder.HasOne(d => d.Medicine).WithMany(p => p.Soldmedicines)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_SoldMedicine_Medicine_MedicineId");
        }
    }
}