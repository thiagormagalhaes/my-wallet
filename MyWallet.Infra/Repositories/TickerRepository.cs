using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;
using System.Linq.Expressions;

namespace MyWallet.Infra.Repositories
{
    public class TickerRepository : Repository<Ticker, long>, ITickerRepository
    {
        public TickerRepository(MyWalletContext context) : base(context) { }

        public override async Task<IQueryable<Ticker>> FilterBy(Expression<Func<Ticker, bool>> filterExpression)
        {
            return await Task.FromResult(_set.Include(x => x.Company).Where(filterExpression));
        }
    }
}
