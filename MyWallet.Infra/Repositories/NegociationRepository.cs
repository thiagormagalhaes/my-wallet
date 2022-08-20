using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;

namespace MyWallet.Infra.Repositories
{
    public class NegociationRepository : Repository<Negociation, long>, INegociationRepository
    {
        public NegociationRepository(MyWalletContext context) : base(context) { }
    }
}
