using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Entities;

namespace MyWallet.Infra.Data.Mappings
{
    public class EarningMapping : IEntityTypeConfiguration<Earning>
    {
        public void Configure(EntityTypeBuilder<Earning> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Ticker)
                .WithMany(x => x.Earnings)
                .HasForeignKey(x => x.TickerId)
                .IsRequired();

            builder.Property(x => x.DividendType)
                .IsRequired();

            builder.Property(x => x.DateCom)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.UnitValue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.TotalValue)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
