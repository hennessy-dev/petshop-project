using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("medicine");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasIndex(e => e.SupplierId, "IX_Medicine_SupplierId");

            builder.Property(e => e.Laboratory)
                .IsRequired()
                .HasMaxLength(110);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(110);
            builder.Property(e => e.Stock).HasColumnName("stock");

            builder.HasOne(d => d.Supplier).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Medicine_Supplier_SupplierId");
        }
    }
}