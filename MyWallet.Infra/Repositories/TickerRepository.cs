using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;

namespace MyWallet.Infra.Repositories
{
    public class TickerRepository : Repository<Ticker>, ITickerRepository
    {
        public TickerRepository(MyWalletContext context) : base(context) { }
    }
}
