using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Entities;

namespace MyWallet.Infra.Data.Mappings
{
    public class RecommendationMapping : IEntityTypeConfiguration<Recommendation>
    {
        public void Configure(EntityTypeBuilder<Recommendation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.RecommendedWallet)
                .WithMany(x => x.Recommendations)
                .HasForeignKey(x => x.RecommendedWalletId)
                .IsRequired();

            builder.HasOne(x => x.Ticker)
                .WithMany()
                .HasForeignKey(x => x.TickerId)
                .IsRequired();

            builder.Property(x => x.Weight)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.LimitePrice)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
