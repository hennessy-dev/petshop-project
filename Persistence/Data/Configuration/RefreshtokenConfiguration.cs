using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class RefreshtokenConfiguration : IEntityTypeConfiguration<Refreshtoken>
    {
        public void Configure(EntityTypeBuilder<Refreshtoken> builder)
        {
            builder.ToTable("refreshToken");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasIndex(e => e.UserId, "IX_RefreshToken_UserId");

            builder.Property(e => e.Created).HasMaxLength(6);
            builder.Property(e => e.Expires).HasMaxLength(6);
            builder.Property(e => e.Revoked).HasMaxLength(6);

            builder.HasOne(d => d.User).WithMany(p => p.Refreshtokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RefreshToken_user_UserId");
        }
    }
}