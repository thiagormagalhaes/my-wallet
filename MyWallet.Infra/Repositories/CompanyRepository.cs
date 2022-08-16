using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;
using System.Linq;

namespace MyWallet.Infra.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(MyWalletContext _context) : base(_context) { }

        public async Task<Company?> GetByCnpjAsync(string cnpj)
        {
            return await _set.Include(x => x.Tickers)
                .FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }
    }
}
