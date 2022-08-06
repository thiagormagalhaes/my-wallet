using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Entities;

namespace MyWallet.Infra.Data.Mappings
{
    public class AdministratorMapping : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasAlternateKey(x => x.Cnpj);

            builder.Property(x => x.Cnpj)
                .HasMaxLength(18)
                .IsRequired();
        }
    }
}
