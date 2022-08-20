using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Entities;

namespace MyWallet.Infra.Data.Mappings
{
    public class RecommendedWalletMapping : IEntityTypeConfiguration<RecommendedWallet>
    {
        public void Configure(EntityTypeBuilder<RecommendedWallet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Weight)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
