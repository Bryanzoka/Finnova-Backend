using Finnova.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finnova.Infrastructure.Persistence.Configurations
{
    public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
    {
        public void Configure(EntityTypeBuilder<VerificationCode> builder)
        {
            builder.ToTable("email_verification_codes");

            builder.HasKey(x => x.Email);

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");

            builder
                .Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("code");

            builder
                .Property(x => x.Expiration)
                .IsRequired()
                .HasColumnName("expiration");
        }
    }
}