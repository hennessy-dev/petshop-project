using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class SoldMedicineConfiguration : IEntityTypeConfiguration<SoldMedicine>
    {
        public void Configure(EntityTypeBuilder<SoldMedicine> builder)
        {
            builder.ToTable("SoldMedicine");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasIndex(e => e.MedicineId, "IX_SoldMedicine_MedicineId");

            builder.Property(e => e.SoldDate).HasColumnType("datetime");

            builder.HasOne(d => d.Medicine).WithMany(p => p.SoldMedicines)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_SoldMedicine_Medicine_MedicineId");
        }
    }
}