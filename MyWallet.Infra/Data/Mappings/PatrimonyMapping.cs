using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Domain.Entities;

namespace MyWallet.Infra.Data.Mappings
{
    internal class PatrimonyMapping : IEntityTypeConfiguration<Patrimony>
    {
        public void Configure(EntityTypeBuilder<Patrimony> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Ticker)
                .WithMany()
                .HasForeignKey(x => x.TickerId)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();
        }
    }
}
