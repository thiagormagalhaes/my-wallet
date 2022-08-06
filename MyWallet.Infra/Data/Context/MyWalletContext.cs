using Microsoft.EntityFrameworkCore;

namespace MyWallet.Infra.Data.Context
{
    public class MyWalletContext : DbContext
    {
        public MyWalletContext(DbContextOptions<MyWalletContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mappingsAssemply = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(mappingsAssemply);

            base.OnModelCreating(modelBuilder);
        }
    }
}
