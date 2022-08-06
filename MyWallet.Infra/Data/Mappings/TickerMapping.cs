using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Entities;

namespace MyWallet.Infra.Data.Mappings
{
    public class TickerMapping : IEntityTypeConfiguration<Ticker>
    {
        public void Configure(EntityTypeBuilder<Ticker> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasAlternateKey(x => x.Code);

            builder.Property(x => x.Code)
                .HasMaxLength(11)
                .IsRequired();

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Tickers)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(18, 2);
        }
    }
}
