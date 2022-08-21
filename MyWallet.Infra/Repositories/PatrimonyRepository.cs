using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;

namespace MyWallet.Infra.Repositories
{
    public class PatrimonyRepository : Repository<Patrimony, Guid>, IPatrimonyRepository
    {
        public PatrimonyRepository(MyWalletContext context) : base(context)
        {
        }

        public override async Task<IList<Patrimony>> GetAll()
        {
            return await _set.Select(x => x)
                .Include(x => x.Ticker)
                    .ThenInclude(x => x.Company)
                .ToListAsync();
        }
    }
}
