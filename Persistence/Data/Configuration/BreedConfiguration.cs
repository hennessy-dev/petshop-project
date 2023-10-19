using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("breed");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("breed");

            builder.HasIndex(e => e.SpeciesId, "IX_Breed_SpeciesId");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(70);

            builder.HasOne(d => d.Species).WithMany(p => p.Breeds)
                .HasForeignKey(d => d.SpeciesId)
                .HasConstraintName("FK_Breed_Species_SpeciesId");
        }
    }
}