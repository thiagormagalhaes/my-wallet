using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;
using System.Linq.Expressions;

namespace MyWallet.Infra.Repositories
{
    public class RecommendedWalletRepository : Repository<RecommendedWallet, long>, IRecommendedWalletRepository
    {
        public RecommendedWalletRepository(MyWalletContext context) : base(context) { }

        public override async Task<IQueryable<RecommendedWallet>> FilterBy(Expression<Func<RecommendedWallet, bool>> filterExpression)
        {
            return await Task.FromResult(_set.Include(x => x.Recommendations).ThenInclude(x => x.Ticker).Where(filterExpression));
        }

        public virtual async Task<IList<RecommendedWallet>> GetAll()
        {
            return await _set.Select(x => x)
                .Include(x => x.Recommendations)
                    .ThenInclude(x => x.Ticker)
                        .ThenInclude(x => x.Company)
                .ToListAsync();
        }
    }
}
