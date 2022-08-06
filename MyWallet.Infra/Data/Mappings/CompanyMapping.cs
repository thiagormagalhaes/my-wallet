using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Entities;

namespace MyWallet.Infra.Data.Mappings
{
    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasAlternateKey(x => x.Cnpj);

            builder.Property(x => x.Cnpj)
                .HasMaxLength(18)
                .IsRequired();

            builder.Property(x => x.Category)
                .IsRequired();

            builder.HasOne(x => x.Administrator)
                .WithMany()
                .HasForeignKey(x => x.AdministratorId)
                .IsRequired(false);
        }
    }
}
