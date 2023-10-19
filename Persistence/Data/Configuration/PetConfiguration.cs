using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pet");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasIndex(e => e.BreedId, "IX_Pet_BreedId");

            builder.HasIndex(e => e.OwnerId, "IX_Pet_OwnerId");

            builder.Property(e => e.BirthDate).HasColumnType("datetime");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);

            builder.HasOne(d => d.Breed).WithMany(p => p.Pets)
                .HasForeignKey(d => d.BreedId)
                .HasConstraintName("FK_Pet_Breed_BreedId");

            builder.HasOne(d => d.Owner).WithMany(p => p.Pets)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK_Pet_Owner_OwnerId");
        }
    }
}