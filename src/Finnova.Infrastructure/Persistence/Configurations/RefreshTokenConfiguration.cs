using Finnova.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finnova.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");

            builder.HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .HasColumnName("id");

            builder
                .Property(r => r.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            builder
                .Property(r => r.Token)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("token");

            builder
                .Property(r => r.CreateAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder
                .Property(r => r.ExpiresAt)
                .IsRequired()
                .HasColumnName("expires_at");

            builder
                .Property(r => r.RevokedAt)
                .IsRequired(false)
                .HasColumnName("revoked_at");

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .IsRequired();
        }
    }
}