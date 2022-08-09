using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Entities;

namespace MyWallet.Infra.Data.Mappings
{
    public class NegociationMapping : IEntityTypeConfiguration<Negociation>
    {
        public void Configure(EntityTypeBuilder<Negociation> builder)
        {
            builder.Property(x => x.DateOperation)
                .IsRequired();

            builder.HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .IsRequired();

            builder.Property(x => x.Operation)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
