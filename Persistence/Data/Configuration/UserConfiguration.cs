using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("password");
            builder.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            builder.HasMany(d => d.Rols).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Userrol",
                    r => r.HasOne<Rol>().WithMany()
                        .HasForeignKey("RolId")
                        .HasConstraintName("FK_userRol_rol_RolId"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_userRol_user_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RolId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("userrol");
                        j.HasIndex(new[] { "RolId" }, "IX_userRol_RolId");
                    });
        }
    }
}