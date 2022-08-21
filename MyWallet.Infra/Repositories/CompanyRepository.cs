using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;
using System.Linq;
using System.Linq.Expressions;

namespace MyWallet.Infra.Repositories
{
    public class CompanyRepository : Repository<Company, long>, ICompanyRepository
    {
        public CompanyRepository(MyWalletContext _context) : base(_context) { }

        public async Task<Company?> GetByCnpjAsync(string cnpj)
        {
            return await _set.Include(x => x.Tickers)
                .FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }

        public override async Task<IQueryable<Company>> FilterBy(Expression<Func<Company, bool>> filterExpression)
        {
            return await Task.FromResult(_set.Include(x => x.Tickers).Include(x => x.Administrator).Where(filterExpression));
        }
    }
}
