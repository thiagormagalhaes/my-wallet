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
    }
}
