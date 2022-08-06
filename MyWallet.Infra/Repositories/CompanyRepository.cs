using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;

namespace MyWallet.Infra.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(MyWalletContext _context) : base(_context) { }
    }
}
