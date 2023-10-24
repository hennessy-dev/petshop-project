using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class PurchasedMedicineConfiguration : IEntityTypeConfiguration<PurchasedMedicine>
    {
        public void Configure(EntityTypeBuilder<PurchasedMedicine> builder)
        {
            builder.ToTable("purchasedMedicine");   

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasIndex(e => e.MedicineId, "IX_PurchasedMedicines_MedicineId");

            builder.HasIndex(e => e.SupplierId, "IX_PurchasedMedicines_SupplierId");

            builder.Property(e => e.PurchaseDate).HasColumnType("datetime");

            builder.HasOne(d => d.Medicine).WithMany(p => p.PurchasedMedicines)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_PurchasedMedicines_Medicine_MedicineId");

            builder.HasOne(d => d.Supplier).WithMany(p => p.PurchasedMedicines)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_PurchasedMedicines_Supplier_SupplierId");
        }
    }
}